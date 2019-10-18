using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MisClases
{
    [Serializable]
    public class Celda
    {
        private double Temperature;
        private double FutureTemperature;
        private double Phase;
        private double FuturePhase;
        //marks these atributes non serializable
        [NonSerialized]private Rectangle rectangleTemp;
        [NonSerialized]private Rectangle rectanglePhase;

        public Celda()
        {
            this.Temperature = -1;
            this.Phase = 1;
            this.FuturePhase = 1;
            this.FutureTemperature = -1;
        }
        public Celda(Rectangle rectangle, Rectangle rectangle2)
        {
            this.Temperature = -1;
            this.Phase = 1;
            this.rectangleTemp = rectangle;
            this.rectanglePhase = rectangle2;
        }

        public Celda(Celda celda)
        {
            this.Temperature = celda.Temperature;
            this.FutureTemperature = celda.FutureTemperature;
            this.Phase = celda.Phase;
            this.FuturePhase = celda.FuturePhase;
            this.rectangleTemp=celda.rectangleTemp;
            this.rectanglePhase = celda.rectanglePhase;
    }

        public Rectangle GetRectangleTemp()
        {
            return rectangleTemp;
        }
        public Rectangle GetRectanglePhase()
        {
            return rectanglePhase;
        }
        public double getTemperature()
        {
            return Temperature;
        }

        public double getFutureTemperature()
        {
            return FutureTemperature;
        }
        public double getPhase()
        {
            return Phase;
        }

        public double getFuturePhase()
        {
            return FuturePhase;
        }

        public void setTemperature(double value)
        {
            this.Temperature=value;
        }

        public void setFutureTemperature(double value)
        {
            this.FutureTemperature = value;
        }
        public void setPhase(double value)
        {
            this.Phase = value;
        }

        public void setFuturePhase(double value)
        {
            this.FuturePhase = value;
        }
        public void change()
        {
            this.Temperature = this.FutureTemperature;
            this.Phase = this.FuturePhase;
        }
        public void setRectangles(Rectangle RecTemp, Rectangle RecPhase)
        {
            this.rectangleTemp = RecTemp;
            this.rectanglePhase = RecPhase;
        }
        public double ComputePhase(Variables var, double ij, double imenos1j, double imas1j, double ijmas1, double ijmenos1, double Tij)
        {
            return ij + 1 / (Math.Pow(var.GetEpsilon(), 2) * var.Getm()) * (ij * (1 - ij) * (ij - 0.5 + 30 * var.GetEpsilon() * var.GetBeta() * var.GetDelta() * Tij * ij * (1 - ij)) + Math.Pow(var.GetEpsilon(), 2) * (((imas1j - 2 * ij + imenos1j) / (Math.Pow(var.GetDeltaX(), 2))) + ((ijmas1 - 2 * ij + ijmenos1) / (Math.Pow(var.GetDeltaY(), 2))))) * var.GetDeltaT();
        }
        public double ComputeTemp(Variables var, double Pij, double Timas1j, double Tij, double Timenos1j, double Tijmas1, double Tijmenos1, double Phase)
        {
            return Tij + (((Timas1j - 2 * Tij + Timenos1j) / (Math.Pow(var.GetDeltaX(), 2))) + ((Tijmas1 - 2 * Tij + Tijmenos1) / (Math.Pow(var.GetDeltaY(), 2))) - ((1 / var.GetDelta()) * (30 * Math.Pow(Pij, 2) - 60 * Math.Pow(Pij, 3) + 30 * Math.Pow(Pij, 4)) * ((Phase - Pij) / var.GetDeltaT()))) * var.GetDeltaT();
        }
        //Fills the cells with the correct color range following a mathematical formula
        public void SetTempColor()
        {
            double num = this.Temperature;
            if (num < -1) { num = -1; }
            if (num > 0) { num = 0; }
            this.rectangleTemp.Fill= new SolidColorBrush(Color.FromArgb(255, 255, Convert.ToByte(Math.Exp(5.54517744448 * -num) - 1), Convert.ToByte(Math.Exp(5.54517744448 * -num) - 1)));
        }
        public void SetPhaseColor()
        {
            double num = this.Phase;
            if (num > 1) { num = 1; }
            if (num < 0) { num = 0; }
            this.rectanglePhase.Fill= new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(Math.Exp(5.54517744448 * num) - 1), 255, Convert.ToByte(Math.Exp(5.54517744448 * num) - 1)));
        }
    }
}
