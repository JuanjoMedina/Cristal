using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace MisClases
{
    public class ExtensionClass
    {    
        public static double GetPhase(Variables var, double ij, double imenos1j, double imas1j, double ijmas1, double ijmenos1,double Tij)
        {
            return ij + 1 / (Math.Pow(var.GetEpsilon(), 2) * var.Getm()) * ij * (1 - ij) * (ij - 0.5 + 30 * var.GetEpsilon() * var.GetBeta() * var.GetDelta() * Tij * ij * (1 - ij)) + Math.Pow(var.GetEpsilon(), 2) * ((imas1j - 2 * ij + imenos1j) / (2 * Math.Pow(var.GetDeltaX(), 2)) + (ijmas1 - 2 * ij + ijmenos1) / (2 * Math.Pow(var.GetDeltaY(), 2)))*var.GetDeltaT();
        }
        public static double GetTemp(Variables var, double Pij, double Timas1j, double Tij, double Timenos1j, double Tijmas1, double Tijmenos1, double Phase)
        {
            return Tij + (Timas1j - 2 * Tij + Timenos1j) / (2 * Math.Pow(var.GetDeltaX(), 2))+(Tijmas1-2*Tij+Tijmenos1)/(2 * Math.Pow(var.GetDeltaY(), 2))-1/var.GetDelta()*(30*Math.Pow(Pij,2)-60*Math.Pow(Pij,3)+30*Math.Pow(Pij,4))*Phase;
        }
        public static SolidColorBrush GetTempColor(double num)
        {
            return new SolidColorBrush(Color.FromArgb(255, 255, Convert.ToByte(Math.Exp(5.54517744448 * -num)-1), Convert.ToByte(Math.Exp(5.54517744448 * -num)-1)));
        }
        public static SolidColorBrush GetPhaseColor(double num)
        {
            return new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(Math.Exp(5.54517744448 * num)-1), 255, Convert.ToByte(Math.Exp(5.54517744448 * num)-1)));
        }

        //public static void Represent(this DataGridView dataGridView,Celda[][] bacterias)
        //{
        //    for (int i = 0; i < bacterias.Length; i++)
        //    {
        //        for (int j = 0; j < bacterias[i].Length; j++)
        //        {
        //            Celda bacteria = bacterias[i][j];
        //            if (bacteria.getViva())
        //            {
        //                dataGridView.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Red;
        //                dataGridView.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.Red;
        //            }
        //            else
        //            {
        //                dataGridView.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
        //                dataGridView.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.White;
        //            }
        //        }
        //    }
        //}
    }
}
