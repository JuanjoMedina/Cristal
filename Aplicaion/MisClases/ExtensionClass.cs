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
            return ij + 1 / (Math.Pow(var.GetEpsilon(), 2) * var.Getm()) * (ij * (1 - ij) * (ij - 0.5 + 30 * var.GetEpsilon() * var.GetBeta() * var.GetDelta() * Tij * ij * (1 - ij)) + Math.Pow(var.GetEpsilon(), 2) * (((imas1j - 2 * ij + imenos1j) / (Math.Pow(var.GetDeltaX(), 2))) + ((ijmas1 - 2 * ij + ijmenos1) / (Math.Pow(var.GetDeltaY(), 2)))))*var.GetDeltaT();
        }
        public static double GetTemp(Variables var, double Pij, double Timas1j, double Tij, double Timenos1j, double Tijmas1, double Tijmenos1, double Phase)
        {
            return Tij + (((Timas1j - 2 * Tij + Timenos1j) / (Math.Pow(var.GetDeltaX(), 2)))+((Tijmas1-2*Tij+Tijmenos1)/(Math.Pow(var.GetDeltaY(), 2)))-((1/var.GetDelta())*(30*Math.Pow(Pij,2)-60*Math.Pow(Pij,3)+30*Math.Pow(Pij,4))*((Phase-Pij)/var.GetDeltaT())))*var.GetDeltaT();
        }
        public static SolidColorBrush GetTempColor(double num)
        {
            double a = num;
            if (num < -1) { num = -1; }
            if (num > 0) { num = 0; }
            return new SolidColorBrush(Color.FromArgb(255, 255, Convert.ToByte(Math.Exp(5.54517744448 * -num)-1), Convert.ToByte(Math.Exp(5.54517744448 * -num)-1)));
        }
        public static SolidColorBrush GetPhaseColor(double num)
        {
            double a=num;
            if (num > 1) { num = 1; }
            if (num < 0) { num = 0; }
            return new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(Math.Exp(5.54517744448 * num)-1), 255, Convert.ToByte(Math.Exp(5.54517744448 * num)-1)));
        }
        public string GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return Convert.ToString(Math.Round(new Random().NextDouble() * (maxNumber - minNumber) + minNumber,8));
        }


    }
}
