using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using MisClases;
using System.IO;
using Microsoft.Win32;
using ZedGraph;

namespace Aplicaion
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        bool Condition = false;
        bool Mirror = true;
        bool Started = false;
        int incrTiempo = 100;
        bool gridSet = false;
        List<double> Tiempo = new List<double>() { 0 };
        Malla cellGrid = new Malla();
        bool Customvar = false;
        bool CustomvarUnmarked = false;
        double[] averages;
        Variables variables;
        PointPairList tempValues = new PointPairList();
        PointPairList phaseValues = new PointPairList();


        public MainWindow()
        {

            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0,0,0,0, incrTiempo);
            timer_label.Visibility = Visibility.Hidden;
            Charts.CreateGraph(tempgraph, tempValues, "Temperature");
            Charts.CreateGraph(phasegraph, phaseValues, "Phase");
        }

        //Sets the grid
        private void SetGrid_Click(object sender, RoutedEventArgs e)
        {
           //Buttons
            SetGrid.IsEnabled = false;
            DiscardGrid.IsEnabled = true;
            ColumnSlider.IsEnabled = false;
            RowsSlider.IsEnabled=false;

            //This indicates that the grid has been set and we can confirm the settings
            gridSet = true;

            //Grid
            cellGrid.setceldas(new Celda[Convert.ToInt32(RowsSlider.Value)+2][]);

            //Adding Columns
            for (int j = 0; j < ColumnSlider.Value; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid2.ColumnDefinitions.Add(new ColumnDefinition());
            }
    
            //Adding Rows
            for (int i = 0; i < RowsSlider.Value+2; i++)
            {
                Celda[] cellRow = new Celda[Convert.ToInt32(ColumnSlider.Value)+2];
                if (i > 0 && i <= RowsSlider.Value)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid2.RowDefinitions.Add(new RowDefinition());

                    //This for loop prints the grid
                    for (int j = 0; j < ColumnSlider.Value + 2; j++)
                    {
                        if (j > 0 && j <= ColumnSlider.Value)
                        {
                            Rectangle rectangle = new Rectangle();
                            Rectangle rectangle2 = new Rectangle();
                            SolidColorBrush BlackBrush = new SolidColorBrush();
                            BlackBrush.Color = Colors.Black;
                            rectangle.Stroke = BlackBrush;
                            rectangle2.Stroke = BlackBrush;
                            rectangle.StrokeThickness = 1;
                            rectangle2.StrokeThickness = 1;
                            rectangle.Fill = new SolidColorBrush(Colors.White);
                            rectangle2.Fill = new SolidColorBrush(Colors.White);
                            //Here we add the rectangles inside the grid
                            Grid.SetRow(rectangle, i - 1);
                            Grid.SetColumn(rectangle, j - 1);
                            grid.Children.Add(rectangle);
                            Grid.SetRow(rectangle2, i - 1);
                            Grid.SetColumn(rectangle2, j - 1);
                            grid2.Children.Add(rectangle2);
                            cellRow[j] = new Celda(rectangle, rectangle2);
                        }
                        //next two elses fill the bounday cells
                        else
                        {
                            cellRow[j] = new Celda();
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < ColumnSlider.Value + 2; j++)
                    {
                        cellRow[j] = new Celda();
                    }

                }
                cellGrid.getceldas()[i] = cellRow;
            }
        }

        //erases the grid and resets it
        private void DiscardGrid_Click(object sender, RoutedEventArgs e)
        {
            SetGrid.IsEnabled = true;
            DiscardGrid.IsEnabled = false;
            ColumnSlider.IsEnabled = true;
            RowsSlider.IsEnabled = true;

            //This indicates that the grid hasn't been set and we cannot confirm the settings
            gridSet = false;

            Rectangle rectangle = (Rectangle)grid.Children[0];
            Rectangle rectangle2 = (Rectangle)grid2.Children[0];

            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            grid2.Children.Clear();
            grid2.ColumnDefinitions.Clear();
            grid2.RowDefinitions.Clear();
            grid.Children.Add(rectangle);
            grid2.Children.Add(rectangle2);
        }


        //When you scroll it changes the number 
        private void RowsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RowsCount.Content = RowsSlider.Value.ToString();
        }

        //When you scroll it changes the number
        private void ColumnSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColumnsCount.Content = ColumnSlider.Value.ToString();
        }

        //It tells us if there is any selection of the boundary conditions
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combobox_Condition.SelectedIndex == 0)
                Condition = false;
            else
            {
                Condition = true;
                if (Combobox_Condition.SelectedIndex == 1)
                    Mirror = true;
                else
                    Mirror = false;
            }
        }

        //It calculates, saves and changes the color of the cells. After that adds the following values to the graph
        private void timer_Tick(object sender, EventArgs e)
        {
            timer_label.Visibility = Visibility.Visible;
            try
            {
                averages = cellGrid.Calcular(Mirror, variables);
                cellGrid.SaveAndSet();
                cellGrid.Represent();
                Tiempo.Add(Math.Round(Tiempo[Tiempo.Count - 1] + variables.GetDeltaT(), 6));
                timer_label.Content =Convert.ToString(Tiempo[Tiempo.Count - 1]) + " s";

                //ZedGRaph
                tempValues.Add(Tiempo[Tiempo.Count - 1], averages[0]);
                phaseValues.Add(Tiempo[Tiempo.Count - 1], averages[1]);
                tempgraph.AxisChange();
                phasegraph.AxisChange();
                tempgraph.Invalidate();
                phasegraph.Invalidate();
            }
            catch
            {
                MessageBox.Show("Something went wrong in your simulation. Possibly the parameters were not suitable and the simulation crashed. Please try with other settings or the predefined ones");
                timer.Stop();
                this.Confirm_Button_Click(new object(),new RoutedEventArgs());
            }
        }


        //Starts the timer
        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            simSpeed.IsEnabled = true;
            timer.Start();
            HelpLabel.Content = "Simulation running";
        }

        //Stops the timer
        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            simSpeed.IsEnabled = false;
            if (timer.IsEnabled)
            {
                timer.Stop();
                HelpLabel.Content = "Simulation paused";
            }
            
        }

        //Gets all the elements out of the stack and resets the graph and the time list
        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            simSpeed.IsEnabled = false;
            timer.Stop();
            HelpLabel.Content = "Simulation restarted";
            timer_label.Visibility = Visibility.Hidden;
            while (cellGrid.getMemory().Count != 0)
            {
                cellGrid.setceldas(cellGrid.getMemory().Pop());
            }
            cellGrid.Represent();
            Tiempo = new List<double>() { 0 };
            timer_label.Content = Convert.ToString(Tiempo[Tiempo.Count - 1]) + " s";

            //ZedGraph
            tempValues.RemoveRange(0,tempValues.Count);
            phaseValues.RemoveRange(0, phaseValues.Count);
            tempgraph.AxisChange();
            phasegraph.AxisChange();
            tempgraph.Invalidate();
            phasegraph.Invalidate();
        }

        //Gets an element out of the stack and rewinds time one step
        private void Button_Atrás_Click(object sender, RoutedEventArgs e)
        {
            simSpeed.IsEnabled = false;
            timer.Stop();
            HelpLabel.Content = "Simulation paused";
            if (cellGrid.getMemory().Count != 0)
            {
                cellGrid.setceldas(cellGrid.getMemory().Pop());
                cellGrid.Represent();

                Tiempo.RemoveAt(Tiempo.Count - 1);
                timer_label.Content = Convert.ToString(Tiempo[Tiempo.Count - 1]) + " s";
            }

            if (tempValues.Count > 0 && phaseValues.Count>0)
            {
                //ZedGraph
                tempValues.RemoveRange(tempValues.Count - 1, 1);
                phaseValues.RemoveRange(phaseValues.Count - 1, 1);
                tempgraph.AxisChange();
                phasegraph.AxisChange();
                tempgraph.Invalidate();
                phasegraph.Invalidate();
            }
        }

        //It does one timer tick
        private void Button_Adelante_Click(object sender, RoutedEventArgs e)
        {
            simSpeed.IsEnabled = false;
            timer.Stop();
            timer_Tick(new object(), new EventArgs());
            HelpLabel.Content = "Simulation paused";
        }

        //Sets predefined settings
        private void Button_Demonstration_Click(object sender, RoutedEventArgs e)
        {
            Rectangle rectangle = (Rectangle)grid.Children[0];
            Rectangle rectangle2 = (Rectangle)grid2.Children[0];

            //This indicates that the grid has been set and we can confirm the settings
            gridSet = true;

            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            grid2.Children.Clear();
            grid2.ColumnDefinitions.Clear();
            grid2.RowDefinitions.Clear();
            grid.Children.Add(rectangle);
            grid2.Children.Add(rectangle2);

            RowsSlider.Value = 9;
            ColumnSlider.Value = 9;
            SetGrid_Click(new object(), new RoutedEventArgs());
            cellGrid.getceldas()[5][5].GetRectanglePhase().StrokeThickness = 3;
            cellGrid.getceldas()[5][5].GetRectangleTemp().StrokeThickness = 3;
            cellGrid.getceldas()[5][5].GetRectanglePhase().Fill = new SolidColorBrush(Color.FromArgb(255,0,255,0));
            cellGrid.getceldas()[5][5].GetRectangleTemp().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            cellGrid.getceldas()[5][5].setPhase(0);
            cellGrid.getceldas()[5][5].setTemperature(0);
            Combobox_Condition.SelectedIndex = 2;
            Combobox_Variables.SelectedIndex = 1;
        }
        //You select the cell where the grain sets
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //First you get the position and then it computes the row and the column where it is
                Point Location = e.GetPosition(grid);
                int row = Convert.ToInt32(Math.Truncate(Location.Y / (grid.Height / Convert.ToDouble(grid.RowDefinitions.Count)))); 
                int column = Convert.ToInt32(Math.Truncate(Location.X / (grid.Width / Convert.ToDouble(grid.ColumnDefinitions.Count))));
                //If the settings haven't been confirmed, the clicked cell will change its status 
                if (!Started)
                {
                    if (cellGrid.getceldas()[row + 1][column + 1].getPhase() != 0)
                    {
                        cellGrid.getceldas()[row + 1][column + 1].GetRectanglePhase().StrokeThickness = 3;
                        cellGrid.getceldas()[row + 1][column + 1].GetRectangleTemp().StrokeThickness = 3;
                        cellGrid.getceldas()[row + 1][column + 1].GetRectanglePhase().Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                        cellGrid.getceldas()[row + 1][column + 1].GetRectangleTemp().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                        cellGrid.getceldas()[row + 1][column + 1].setPhase(0);
                        cellGrid.getceldas()[row + 1][column + 1].setTemperature(0);
                    }
                    else
                    {
                        cellGrid.getceldas()[row + 1][column + 1].GetRectanglePhase().StrokeThickness = 1;
                        cellGrid.getceldas()[row + 1][column + 1].GetRectangleTemp().StrokeThickness = 1;
                        cellGrid.getceldas()[row + 1][column + 1].GetRectanglePhase().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        cellGrid.getceldas()[row + 1][column + 1].GetRectangleTemp().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        cellGrid.getceldas()[row + 1][column + 1].setPhase(1);
                        cellGrid.getceldas()[row + 1][column + 1].setTemperature(-1);
                    }
                }

            }
            catch { }
        }
        //It does the same as the previous method but for the grid 2
        private void Grid2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point Location = e.GetPosition(grid2);
                int row = Convert.ToInt32(Math.Truncate(Location.Y / (grid2.Height / Convert.ToDouble(grid2.RowDefinitions.Count)))); 
                int column = Convert.ToInt32(Math.Truncate(Location.X / (grid2.Width / Convert.ToDouble(grid2.ColumnDefinitions.Count))));
                if (!Started)
                {
                    if (cellGrid.getceldas()[row + 1][column + 1].getPhase() != 0)
                    {
                        cellGrid.getceldas()[row + 1][column + 1].GetRectanglePhase().Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                        cellGrid.getceldas()[row + 1][column + 1].GetRectangleTemp().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                        cellGrid.getceldas()[row + 1][column + 1].setPhase(0);
                        cellGrid.getceldas()[row + 1][column + 1].setTemperature(0);
                    }
                    else
                    {
                        cellGrid.getceldas()[row + 1][column + 1].GetRectanglePhase().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        cellGrid.getceldas()[row + 1][column + 1].GetRectangleTemp().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        cellGrid.getceldas()[row + 1][column + 1].setPhase(1);
                        cellGrid.getceldas()[row + 1][column + 1].setTemperature(-1);
                    }
                }

            }
            catch { }
        }
        //If all the parameters have been set the simulation can start. Once it has been confirmed this button will reset all the parameters
        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Confirm_Button.Content.ToString()=="Confirm Configuration")
            {
                if (gridSet)
                {
                    if (Condition)
                    {
                        if (variables != null && !CustomvarUnmarked)
                        {
                            if (cellGrid.HayGrano())
                            {
                                Started = true;
                                //Buttons
                                SetGrid.IsEnabled = false;
                                DiscardGrid.IsEnabled = false;
                                ColumnSlider.IsEnabled = false;
                                RowsSlider.IsEnabled = false;
                                button_Demonstration.IsEnabled = false;

                                button_Play.IsEnabled = true;
                                button_Pause.IsEnabled = true;
                                button_Atrás.IsEnabled = true;
                                button_Adelante.IsEnabled = true;
                                button_Stop.IsEnabled = true;

                                Combobox_Condition.IsEnabled = false;
                                Combobox_Variables.IsEnabled = false;
                                Custom_Variables.IsEnabled = false;

                                button_Save.Visibility = Visibility.Visible;
                                button_Load.Visibility = Visibility.Hidden;

                                Confirm_Button.Content = "Reset Configuration";
                                HelpLabel.Content = "";

                                //Style
                                Confirm_Button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 110, 110));
                                LabelMostrar.Content = "Move Over The\nGrid To See Values";
                                LabelMostrar.FontSize = 14;
                            }
                            else
                            {
                                MessageBox.Show("Select the Starting Point/s First!");
                            }
                        }
                        else
                        {
                            if (variables==null)
                                MessageBox.Show("Select the Variables First!");
                            else
                                MessageBox.Show("You have unmarked your custom variables, please select them or select the predefined variables");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Select the Boundary Condition First!");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Set the Gird First!");
                }
                
            }
            else
            {
                //Unsetting variables
                cellGrid = new Malla();
                //We set started to false because the simulation has ended and we set custom variables to false
                Started = false;
                if (Customvar)
                {
                    Customvar = false;
                    CustomvarUnmarked = false;
                    Combobox_Variables.Items.RemoveAt(3);
                    Combobox_Variables.SelectedIndex = 0;
                }
                //Buttons
                SetGrid.IsEnabled = true;
                DiscardGrid.IsEnabled = true;
                ColumnSlider.IsEnabled = true;
                RowsSlider.IsEnabled = true;
                button_Demonstration.IsEnabled = true;

                button_Play.IsEnabled = false;
                button_Pause.IsEnabled = false;
                button_Atrás.IsEnabled = false;
                button_Adelante.IsEnabled = false;
                button_Stop.IsEnabled = false;
                simSpeed.IsEnabled = false;

                Combobox_Condition.IsEnabled = true;
                Combobox_Variables.IsEnabled = true;
                Custom_Variables.IsEnabled = true;

                button_Save.Visibility = Visibility.Hidden;
                button_Load.Visibility = Visibility.Visible;

                timer.Stop();
                timer_label.Visibility = Visibility.Hidden;
                DiscardGrid_Click(new object(), new RoutedEventArgs());

                Tiempo = new List<double>() { 0 };

                Confirm_Button.Content = "Confirm Configuration";
                HelpLabel.Content="Build the environment first!";

                //Style
                Confirm_Button.Background = new SolidColorBrush(Color.FromArgb(255, 160, 255, 110));

                //ZedGraph
                tempValues.RemoveRange(0, tempValues.Count);
                phaseValues.RemoveRange(0, phaseValues.Count);
                tempgraph.AxisChange();
                phasegraph.AxisChange();
                tempgraph.Invalidate();
                phasegraph.Invalidate();
            }
          
        }
        //Opens a new window to create the custom variables. If there are some created will be rewritten
        private void Custom_Variables_Click(object sender, RoutedEventArgs e)
        {
            CustomVariables c = new CustomVariables();
            c.ShowDialog();
            if (c.getSet())
            {
                if (Customvar)
                {
                    Combobox_Variables.SelectedIndex = 0;
                    Combobox_Variables.Items.RemoveAt(3);
                }
                variables = c.Getvars();
                Customvar = true;
                Combobox_Variables.Items.Add("Custom variables");
                Combobox_Variables.SelectedIndex = 3;

            }
        }

        private void Combobox_Variables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If the selected index aren't the custom variables or the "select variables" it searches for the existence of some custom variables and it asks the user to delete them in order to set one of the predefined variables
            if (Combobox_Variables.SelectedIndex != 0 && Combobox_Variables.SelectedIndex != 3)
            {
                if (Customvar)
                {
                    MessageBoxButton buttons = MessageBoxButton.OKCancel;
                    MessageBoxResult result = MessageBox.Show("You will erase your custom variables", "Warning", buttons, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        CustomvarUnmarked = false;
                        Customvar = false;
                        Combobox_Variables.Items.RemoveAt(3);
                        if (Combobox_Variables.SelectedIndex == 1)
                            variables = new Variables(true);
                        else if (Combobox_Variables.SelectedIndex == 2)
                            variables = new Variables(false);
                    }
                    else
                        Combobox_Variables.SelectedIndex = 3;
                }
                else
                {
                    if (Combobox_Variables.SelectedIndex == 1)
                        variables = new Variables(true);
                    else if (Combobox_Variables.SelectedIndex == 2)
                        variables = new Variables(false);
                }
            }
            //If you select "select variables" and there are not any custom variables, the variables of the system are set to null. If there are custom variables and the user selects "select variables " the bool "unmarked custom variables" sets true.
            else if (Combobox_Variables.Items.Count != 4)
                variables = null;
            else if (Combobox_Variables.Items.Count == 4)
                if (Combobox_Variables.SelectedIndex == 0)
                    CustomvarUnmarked = true;
                else
                    CustomvarUnmarked = false;

        }
       //Creates a new instance of Class Guardado in order to save all its atributes into a binary file using serialization
        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            simSpeed.IsEnabled = false;
            try
            {
                Guardado Guardar = new Guardado(cellGrid, variables,Mirror,Tiempo,tempValues,phaseValues);
                SaveFileDialog Dialogo = new SaveFileDialog();
                if (Dialogo.ShowDialog() == true)
                {
                    BinaryFormatter Bf = new BinaryFormatter();
                    FileStream stream = File.OpenWrite(Dialogo.FileName);
                    Bf.Serialize(stream, Guardar);
                    stream.Close();
                }
            }
            catch
            {
                MessageBox.Show("There was a problem saving");
            }
        }
        //Loads the file and initializes all settings with the information of the file
        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog Dialogo2 = new OpenFileDialog();
                if (Dialogo2.ShowDialog() == true)
                {
                    BinaryFormatter Bf = new BinaryFormatter();
                    FileStream r = File.OpenRead(Dialogo2.FileName);
                    Guardado bac = (Guardado)Bf.Deserialize(r);
                    r.Close();
                    Mirror = bac.getMirror();
                    variables = bac.getVariables();
                    cellGrid = bac.getGrid();
                    Tiempo = bac.getTiempo();

                    //Plots
                    tempValues = bac.getTemp();
                    phaseValues = bac.getPhase();
                    Charts.CreateGraph(tempgraph, tempValues, "Temperature");
                    Charts.CreateGraph(phasegraph, phaseValues, "Phase");

                    ColumnSlider.Value = cellGrid.getceldas()[0].Length-2;
                    RowsSlider.Value = cellGrid.getceldas().Length-2;

                    //Restarting Grid
                    grid.Children.Clear();
                    grid.ColumnDefinitions.Clear();
                    grid.RowDefinitions.Clear();
                    grid2.Children.Clear();
                    grid2.ColumnDefinitions.Clear();
                    grid2.RowDefinitions.Clear();



                    //Adding Columns
                    for (int j = 0; j < ColumnSlider.Value; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid2.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    //We convert the stack to list in order to serach for the first cells
                    Celda[][][] temp = cellGrid.getMemory().ToArray();

                    //Adding Rows
                    Rectangle[][] rectanglesTemp = new Rectangle[cellGrid.getceldas().Length][];
                    Rectangle[][] rectanglesPhase = new Rectangle[cellGrid.getceldas().Length][];
                    for (int i = 1; i < RowsSlider.Value + 1; i++)
                    {
                        Rectangle[] CellRowTemp = new Rectangle[cellGrid.getceldas()[0].Length];
                        Rectangle[] CellRowPhase = new Rectangle[cellGrid.getceldas()[0].Length];
                        grid.RowDefinitions.Add(new RowDefinition());
                        grid2.RowDefinitions.Add(new RowDefinition());

                        //This for loop prints the grid
                        for (int j = 1; j < ColumnSlider.Value + 1; j++)
                        {

                            Rectangle rectangle = new Rectangle();
                            Rectangle rectangle2 = new Rectangle();
                            SolidColorBrush BlackBrush = new SolidColorBrush();
                            BlackBrush.Color = Colors.Black;
                            rectangle.Stroke = BlackBrush;
                            rectangle2.Stroke = BlackBrush;

                            if (temp[temp.Length - 1][i][j].getPhase() == 0)
                            {
                                rectangle.StrokeThickness = 3;
                                rectangle2.StrokeThickness = 3;
                            }
                            else
                            {
                                rectangle.StrokeThickness = 1;
                                rectangle2.StrokeThickness = 1;
                            }

                            rectangle.Fill = new SolidColorBrush(Colors.White);
                            rectangle2.Fill = new SolidColorBrush(Colors.White);
                            //Here we add the rectangles inside the grid
                            Grid.SetRow(rectangle, i-1);
                            Grid.SetColumn(rectangle, j-1);
                            grid.Children.Add(rectangle);
                            Grid.SetRow(rectangle2, i-1);
                            Grid.SetColumn(rectangle2, j-1);
                            grid2.Children.Add(rectangle2);
                            CellRowTemp[j] = rectangle;
                            CellRowPhase[j] = rectangle2;

                        }
                        rectanglesTemp[i] = CellRowTemp;
                        rectanglesPhase[i] = CellRowPhase;
                    }
                    cellGrid.loadFixRectangles(rectanglesTemp, rectanglesPhase);
                    cellGrid.Represent(); 

                    //Style
                    SetGrid.IsEnabled = false;
                    DiscardGrid.IsEnabled = false;
                    ColumnSlider.IsEnabled = false;
                    RowsSlider.IsEnabled = false;
                    button_Demonstration.IsEnabled = false;

                    button_Play.IsEnabled = true;
                    button_Pause.IsEnabled = true;
                    button_Atrás.IsEnabled = true;
                    button_Adelante.IsEnabled = true;
                    button_Stop.IsEnabled = true;

                    Combobox_Condition.IsEnabled = false;
                    Combobox_Variables.IsEnabled = false;
                    Custom_Variables.IsEnabled = false;

                    button_Save.Visibility = Visibility.Visible;
                    button_Load.Visibility = Visibility.Hidden;

                    Confirm_Button.Content = "Reset Configuration";
                    HelpLabel.Content = "";

                    //Style
                    Confirm_Button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 110, 110));

                    //ZedGRaph
                    tempgraph.AxisChange();
                    phasegraph.AxisChange();
                    tempgraph.Invalidate();
                    phasegraph.Invalidate();

                    //Simulation started
                    Started = true;
                }
        }
            catch
            {
                MessageBox.Show("There was a problem loading");
            }

        }
        private void MkLarge1_Click(object sender, RoutedEventArgs e)
        {
            Thickness bigT = new Thickness(365, 0, 160, 220);
            Thickness smT1 = new Thickness(29, 333, 853, 201);
            Thickness smT2 = new Thickness(27, 506, 855, 31);

            phasePlot.Margin = smT2;
            mkLarge2.Content = "Make Larger";
            if (mkLarge1.Content.ToString() == "Make Larger")
            {
                mkLarge1.Content = "Make Small";
                tempPlot.Margin = bigT;
            }
            else
            {
                mkLarge1.Content = "Make Larger";
                tempPlot.Margin = smT1;
            }
        }

        private void MkLarge2_Click(object sender, RoutedEventArgs e)
        {
            Thickness bigT = new Thickness(365, 0, 160, 220);
            Thickness smT1 = new Thickness(29, 333, 853, 201);
            Thickness smT2 = new Thickness(27, 506, 855, 31);

            tempPlot.Margin = smT1;
            mkLarge1.Content = "Make Larger";
            if (mkLarge2.Content.ToString() == "Make Larger")
            {
                mkLarge2.Content = "Make Small";
                phasePlot.Margin = bigT;
            }
            else
            {
                mkLarge2.Content = "Make Larger";
                phasePlot.Margin = smT2;
            }
        }
        //When you move over the grid there's a label that shows the cell temperature or phase
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point Location = e.GetPosition(grid);
            int row = Convert.ToInt32(Math.Truncate(Location.Y / (grid.Height / Convert.ToDouble(grid.RowDefinitions.Count))));
            int column = Convert.ToInt32(Math.Truncate(Location.X / (grid.Width / Convert.ToDouble(grid.ColumnDefinitions.Count))));
            if (Started)
            {
                LabelMostrar1.Content = "Temperature:";
                LabelMostrar.Content = Math.Truncate(cellGrid.getceldas()[row + 1][column + 1].getTemperature() * 1000) / 1000;
                LabelMostrar.FontSize = 20;
                LabelMostrar3.Content = "Phase:";
                LabelMostrar4.Content = Math.Truncate(cellGrid.getceldas()[row + 1][column + 1].getPhase() * 1000) / 1000;
                LabelMostrar4.FontSize = 20;
            }
        }

        private void Grid2_MouseMove(object sender, MouseEventArgs e)
        {
            Point Location = e.GetPosition(grid2);
            int row = Convert.ToInt32(Math.Truncate(Location.Y / (grid2.Height / Convert.ToDouble(grid2.RowDefinitions.Count))));
            int column = Convert.ToInt32(Math.Truncate(Location.X / (grid2.Width / Convert.ToDouble(grid2.ColumnDefinitions.Count))));
            if (Started)
            {
                LabelMostrar1.Content = "Temperature:";
                LabelMostrar.Content = Math.Truncate(cellGrid.getceldas()[row + 1][column + 1].getTemperature() * 1000) / 1000;
                LabelMostrar.FontSize = 20;
                LabelMostrar3.Content = "Phase:";
                LabelMostrar4.Content = Math.Truncate(cellGrid.getceldas()[row + 1][column + 1].getPhase() * 1000) / 1000;
                LabelMostrar4.FontSize = 20;
            }
        }
        //When you move out of the grid the label shows ""
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Started)
            {
                LabelMostrar.Content = "";
                LabelMostrar1.Content = "";
                LabelMostrar3.Content = "";
                LabelMostrar4.Content = "";
            }
        }

        private void Grid2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Started)
            {
                LabelMostrar.Content = "";
                LabelMostrar1.Content = "";
                LabelMostrar3.Content = "";
                LabelMostrar4.Content = "";
            }
        }

        //Slider to change simulation speed
        private void SimSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            timer.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(incrTiempo / slider.Value));
            sliderValue.Text = "x" + simSpeed.Value.ToString();
            
        }
    }
}
