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
using MisClases;

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
        Variables variables;
        
        public MainWindow()
        {

            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0,0,0,0, incrTiempo);
            timer_label.Visibility = Visibility.Hidden;
        }

        //Hace void
        private void SetGrid_Click(object sender, RoutedEventArgs e)
        {
           //Buttons
            SetGrid.IsEnabled = false;
            DiscardGrid.IsEnabled = true;
            ColumnSlider.IsEnabled = false;
            RowsSlider.IsEnabled=false;

            //Nos indica que la grid se ha seteado y por lo tanto se puede confirmar la configuración
            gridSet = true;

            //Grid
            Rectangle[][] GraphicGrid = new Rectangle[Convert.ToInt32(RowsSlider.Value)][];
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
                if (i > 0 && i <= ColumnSlider.Value)
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

        //Borra la malla y hace un reset
        private void DiscardGrid_Click(object sender, RoutedEventArgs e)
        {
            SetGrid.IsEnabled = true;
            DiscardGrid.IsEnabled = false;
            ColumnSlider.IsEnabled = true;
            RowsSlider.IsEnabled = true;

            //Nos indica que la grid se ha des-seteado y por lo tanto NO se puede confirmar la configuración
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


        //Cambia el número al arrastrar 
        private void RowsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RowsCount.Content = RowsSlider.Value.ToString();
        }

        //Cambia el número al arrastrar 
        private void ColumnSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColumnsCount.Content = ColumnSlider.Value.ToString();
        }

        //Nos avisa si hay alguna selección de Boundary Conditions
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

        //Pinta las celdas Color.FromArgb(255, 255, 0, 0)
        private void timer_Tick(object sender, EventArgs e)
        {
            cellGrid.Calcular(Mirror, variables);
            cellGrid.SaveAndSet();
            cellGrid.Represent();
            Started = true;
            Tiempo.Add(Math.Round(Tiempo[Tiempo.Count - 1] + 0.000005,6));
            timer_label.Content = Convert.ToString(Tiempo[Tiempo.Count-1])+" s";
        }

        //Empieza el timer
        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            HelpLabel.Content = "Simulation running";
            timer_label.Visibility = Visibility.Visible;
        }

        //Para el timer
        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                HelpLabel.Content = "Simulation paused";
            }
            
        }

        //Saca todos los elementos de la pila
        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            HelpLabel.Content = "Simulation ended";
            timer_label.Visibility = Visibility.Hidden;
        }

        //Saca un elemento de la pila
        private void Button_Atrás_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            HelpLabel.Content = "Simulation paused";
            if (cellGrid.getMemory().Count != 0)
            {
                cellGrid.setceldas(cellGrid.getMemory().Pop());
                cellGrid.Represent();
            }
                
        }

        //Añade un elemento de la pila
        private void Button_Adelante_Click(object sender, RoutedEventArgs e)
        {
            timer_Tick(new object(), new EventArgs());

            HelpLabel.Content = "Simulation paused";
        }

        //Nos muestra una prueba
        private void Button_Demonstration_Click(object sender, RoutedEventArgs e)
        {
            Rectangle rectangle = (Rectangle)grid.Children[0];
            Rectangle rectangle2 = (Rectangle)grid2.Children[0];

            //Nos indica que la grid se ha seteado y por lo tanto se puede confirmar la configuración
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
            cellGrid.getceldas()[5][5].GetRectanglePhase().Fill = new SolidColorBrush(Color.FromArgb(255,0,255,0));
            cellGrid.getceldas()[5][5].GetRectangleTemp().Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            cellGrid.getceldas()[5][5].setPhase(0);
            cellGrid.getceldas()[5][5].setTemperature(0);
            Combobox_Condition.SelectedIndex = 2;
            Combobox_Variables.SelectedIndex = 1;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point Location = e.GetPosition(grid);
                int row = Convert.ToInt32(Math.Truncate(Location.Y / (grid.Height / Convert.ToDouble(grid.ColumnDefinitions.Count))));
                int column = Convert.ToInt32(Math.Truncate(Location.X / (grid.Width / Convert.ToDouble(grid.RowDefinitions.Count))));
                if (Started)
                {
                    MessageBox.Show(cellGrid.getceldas()[row + 1][column + 1].getTemperature().ToString());
                }
                else
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

        private void Grid2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point Location = e.GetPosition(grid2);
                int row = Convert.ToInt32(Math.Truncate(Location.Y / (grid.Height / Convert.ToDouble(grid.ColumnDefinitions.Count))));
                int column = Convert.ToInt32(Math.Truncate(Location.X / (grid.Width / Convert.ToDouble(grid.RowDefinitions.Count))));
                if (Started)
                {
                    MessageBox.Show(cellGrid.getceldas()[row + 1][column + 1].getPhase().ToString());
                }
                else
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

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Confirm_Button.Content.ToString()=="Confirm Configuration")
            {
                if (gridSet)
                {
                    if (Condition)
                    {
                        if (variables != null)
                        {
                            if (cellGrid.HayGrano())
                            {
                                //Buttons
                                SetGrid.IsEnabled = false;
                                DiscardGrid.IsEnabled = false;
                                ColumnSlider.IsEnabled = false;
                                RowsSlider.IsEnabled = false;

                                button_Play.IsEnabled = true;
                                button_Pause.IsEnabled = true;
                                button_Atrás.IsEnabled = true;
                                button_Adelante.IsEnabled = true;
                                button_Stop.IsEnabled = true;

                                Combobox_Condition.IsEnabled = false;

                                Confirm_Button.Content = "Reset Configuration";
                                HelpLabel.Content = "";

                                //Style
                                Confirm_Button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 110, 110));
                            }
                            else
                            {
                                MessageBox.Show("Select the Starting Point/s First!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select the Variables First!");
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
                //We set started to false because the simulation has ended
                Started = false;
                //Buttons
                SetGrid.IsEnabled = true;
                DiscardGrid.IsEnabled = true;
                ColumnSlider.IsEnabled = true;
                RowsSlider.IsEnabled = true;

                button_Play.IsEnabled = false;
                button_Pause.IsEnabled = false;
                button_Atrás.IsEnabled = false;
                button_Adelante.IsEnabled = false;
                button_Stop.IsEnabled = false;

                Combobox_Condition.IsEnabled = true;

                timer.Stop();
                DiscardGrid_Click(new object(), new RoutedEventArgs());

                Confirm_Button.Content = "Confirm Configuration";
                HelpLabel.Content="Build the environment first!";

                //Style
                Confirm_Button.Background = new SolidColorBrush(Color.FromArgb(255, 160, 255, 110));
            }
          
        }

        private void Custom_Variables_Click(object sender, RoutedEventArgs e)
        {
            CustomVariables c = new CustomVariables();
            c.Show();
        }

        private void Combobox_Variables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combobox_Variables.SelectedIndex != 0)
            {
                if (Customvar)
                {
                    MessageBoxButton buttons = MessageBoxButton.OKCancel;
                    MessageBoxResult result = MessageBox.Show("You will erase your custom variables", "Warning", buttons, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        if (Combobox_Variables.SelectedIndex == 1)
                            variables = new Variables(true);
                        else
                            variables = new Variables(false);
                    }
                    else
                        Combobox_Variables.SelectedIndex = 0;
                }
                else
                {
                    if (Combobox_Variables.SelectedIndex == 1)
                        variables = new Variables(true);
                    else
                        variables = new Variables(false);
                }
            }
        }
    }
}
