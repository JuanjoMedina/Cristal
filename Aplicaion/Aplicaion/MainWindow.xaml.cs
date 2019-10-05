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

namespace Aplicaion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle[][] GraphicGrid;
        public MainWindow()
        {

            InitializeComponent();

        }

        private void SetGrid_Click(object sender, RoutedEventArgs e)
        {
            SetGrid.IsEnabled = false;
            DiscardGrid.IsEnabled = true;
            ColumnSlider.IsEnabled = false;
            RowsSlider.IsEnabled=false;

            GraphicGrid = new Rectangle[Convert.ToInt32(RowsSlider.Value)][];
            for (int j=0;j<ColumnSlider.Value;j++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < RowsSlider.Value; i++)
            {
                Rectangle[] GraphicRow = new Rectangle[Convert.ToInt32(ColumnSlider.Value)];
                grid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < ColumnSlider.Value; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    //SolidColorBrush RedBrush = new SolidColorBrush();
                    //RedBrush.Color = Colors.Red;
                    //rectangle.Fill = RedBrush;
                    Grid.SetRow(rectangle, i);
                    Grid.SetColumn(rectangle, j);
                    grid.Children.Add(rectangle);
                    GraphicRow[j] = rectangle;
                }
                GraphicGrid[i] = GraphicRow;

            }
        }

        private void DiscardGrid_Click(object sender, RoutedEventArgs e)
        {
            SetGrid.IsEnabled = true;
            DiscardGrid.IsEnabled = false;
            ColumnSlider.IsEnabled = true;
            RowsSlider.IsEnabled = true;
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
    }
}
