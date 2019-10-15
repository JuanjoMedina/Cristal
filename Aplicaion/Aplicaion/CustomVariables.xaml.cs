using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MisClases;

namespace Aplicaion
{
    /// <summary>
    /// Lógica de interacción para CustomVariables.xaml
    /// </summary>
    public partial class CustomVariables : Window
    {
        ExtensionClass ext = new ExtensionClass();
        Variables var;
        public CustomVariables()
        {
            InitializeComponent();
        }

        private void Custom_Variables_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var epsilon = Convert.ToDouble(epsilon_box.Text);
                var m = Convert.ToDouble(m_box.Text);
                var beta = Convert.ToDouble(beta_box.Text);
                var delta = Convert.ToDouble(delta_box.Text);
                var deltat = Convert.ToDouble(deltat_box.Text);
                var deltax = Convert.ToDouble(deltax_box.Text);
                var deltay = Convert.ToDouble(deltay_box.Text);

                var = new Variables(epsilon, m, beta, deltat, deltax, deltay, delta);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Some variable/s is/are incorrect!");
            }


            
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            epsilon_box.Text = ext.GetRandomNumberInRange(0.004, 0.006);
            beta_box.Text = ext.GetRandomNumberInRange(350, 450);
            delta_box.Text = ext.GetRandomNumberInRange(0.3, 0.7);
            deltax_box.Text = ext.GetRandomNumberInRange(0.004, 0.006);
            deltay_box.Text = ext.GetRandomNumberInRange(0.004, 0.006);
            m_box.Text = ext.GetRandomNumberInRange(15, 25);
            deltat_box.Text = ext.GetRandomNumberInRange(0.000004, 0.000006);
        }
        public Variables Getvars()
        {
            return var;
        }
    }
}
