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
        Rectangle[][] GraphicGrid;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        bool Condition = false;
        Malla cellGrid = new Malla();
        
        public MainWindow()
        {

            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0,0,0,0,100);

        }

        //Hace void
        private void SetGrid_Click(object sender, RoutedEventArgs e)
        {
            //Buttons
            SetGrid.IsEnabled = false;
            DiscardGrid.IsEnabled = true;
            ColumnSlider.IsEnabled = false;
            RowsSlider.IsEnabled=false;

            button_Play.IsEnabled = true;
            button_Pause.IsEnabled = true;
            button_Atrás.IsEnabled = true;
            button_Adelante.IsEnabled = true;
            button_Stop.IsEnabled = true;

            //Grid
            GraphicGrid = new Rectangle[Convert.ToInt32(RowsSlider.Value)][];
            cellGrid.setceldas(new Celda[Convert.ToInt32(RowsSlider.Value)+2][]);

            //Relleno de Columnas
            for (int j = 0; j < ColumnSlider.Value; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid2.ColumnDefinitions.Add(new ColumnDefinition());
            }
    
            //Adding las Rows
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
                cellGrid.getceldas()[i] = cellRow;
            }
        }

        //Esconde la malla y hace un reset
        private void DiscardGrid_Click(object sender, RoutedEventArgs e)
        {
            SetGrid.IsEnabled = true;
            DiscardGrid.IsEnabled = false;
            ColumnSlider.IsEnabled = true;
            RowsSlider.IsEnabled = true;

            button_Play.IsEnabled = false;
            button_Pause.IsEnabled = false;
            button_Atrás.IsEnabled = false;
            button_Adelante.IsEnabled = false;
            button_Stop.IsEnabled = false;

            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            grid2.Children.Clear();
            grid2.ColumnDefinitions.Clear();
            grid2.RowDefinitions.Clear();
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
            if (Combobox_Condition.SelectedIndex != 0)
                Condition = true;
            else
                Condition = false;
        }

        //Pinta las celdas
        private void timer_Tick(object sender, EventArgs e)
        {
            cellGrid.getceldas()[1][1].GetRectangleTemp().Fill = new SolidColorBrush(System.Windows.Media.Colors.Blue);
            cellGrid.getceldas()[3][3].GetRectanglePhase().Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
        }

        //Empieza el timer
        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        //Para el timer
        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        //Saca todos los elementos de la pila
        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        //Saca un elemento de la pila
        private void Button_Atrás_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        //Añade un elemento de la pila
        private void Button_Adelante_Click(object sender, RoutedEventArgs e)
        {
            timer_Tick(new object(), new EventArgs());
        }

        //Nos muestra una prueba
        private void Button_Demonstration_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            RowsSlider.Value = 9;
            ColumnSlider.Value = 9;
            SetGrid_Click(new object(), new RoutedEventArgs());
        }

        private void Compare_Results_Click(object sender, RoutedEventArgs e)
        {
            Window comparator = new Comparator();
            comparator.Show();
        }
    }
}
