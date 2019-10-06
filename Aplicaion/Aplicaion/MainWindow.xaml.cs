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

        private void SetGrid_Click(object sender, RoutedEventArgs e)
        {
            SetGrid.IsEnabled = false;
            DiscardGrid.IsEnabled = true;
            ColumnSlider.IsEnabled = false;
            RowsSlider.IsEnabled=false;

            button_Play.IsEnabled = true;
            button_Pause.IsEnabled = true;
            button_Atrás.IsEnabled = true;
            button_Adelante.IsEnabled = true;
            button_Stop.IsEnabled = true;

            GraphicGrid = new Rectangle[Convert.ToInt32(RowsSlider.Value)][];
            cellGrid.setceldas(new Celda[Convert.ToInt32(RowsSlider.Value)][]);

            for (int j=0;j<ColumnSlider.Value;j++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < RowsSlider.Value; i++)
            {
                Rectangle[] GraphicRow = new Rectangle[Convert.ToInt32(ColumnSlider.Value)];
                Celda[] cellRow = new Celda[Convert.ToInt32(ColumnSlider.Value)];
                grid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < ColumnSlider.Value; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    SolidColorBrush BlackBrush = new SolidColorBrush();
                    BlackBrush.Color = Colors.Black;
                    rectangle.Stroke = BlackBrush;
                    Grid.SetRow(rectangle, i);
                    Grid.SetColumn(rectangle, j);
                    grid.Children.Add(rectangle);
                    GraphicRow[j] = rectangle;
                    cellRow[j] = new Celda(rectangle);
                }
                GraphicGrid[i] = GraphicRow;
                cellGrid.getceldas()[i] = cellRow;
            }
        }

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
        }



        private void RowsSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RowsCount.Content = RowsSlider.Value.ToString();
        }

        private void ColumnSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColumnsCount.Content = ColumnSlider.Value.ToString();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combobox_Condition.SelectedIndex != 0)
                Condition = true;
            else
                Condition = false;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            cellGrid.getceldas()[0][0].GetRectangle().Fill = new SolidColorBrush(System.Windows.Media.Colors.Blue);
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Button_Atrás_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Button_Adelante_Click(object sender, RoutedEventArgs e)
        {
            timer_Tick(new object(), new EventArgs());
        }

        private void Button_Demonstration_Click(object sender, RoutedEventArgs e)
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            RowsSlider.Value = 9;
            ColumnSlider.Value = 9;
            SetGrid_Click(new object(), new RoutedEventArgs());
        }
    }
}
