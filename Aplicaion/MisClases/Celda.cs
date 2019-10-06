using System;
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
        private Rectangle rectangle;

        public Celda()
        {

        }
        public Celda(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        public Celda(Celda celda)
        {
            this.Temperature = celda.Temperature;
            this.FutureTemperature = celda.FutureTemperature;
            this.Phase = celda.Phase;
            this.FuturePhase = celda.FuturePhase;
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
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
    }
}
