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
    }
}
