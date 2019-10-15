using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    [Serializable]
    public class Variables
    {
        double Epsilon;
        double m;
        double Beta;
        double Delta;
        double DeltaT;
        double DeltaX;
        double DeltaY;

        public Variables(bool Parameters1)
        {
            if (Parameters1)
            {
                this.Epsilon = 0.005;
                this.m = 20;
                this.Beta = 400;
                this.DeltaT = 5*Math.Pow(10,-6);
                this.DeltaX = 0.005;
                this.DeltaY = 0.005;
                this.Delta=0.5;
            }
            else
            {
                this.Epsilon = 0.005;
                this.m = 30;
                this.Beta = 300;
                this.DeltaT = 5 * Math.Pow(10, -6);
                this.DeltaX = 0.005;
                this.DeltaY = 0.005;
                this.Delta = 0.7;
            }
        }
        public Variables(double Epsilon, double m, double Beta, double DeltaT, double DeltaX, double DeltaY,double Delta)
        {
            this.Epsilon = Epsilon;
            this.m = m;
            this.Beta = Beta;
            this.DeltaT = DeltaT;
            this.DeltaX = DeltaX;
            this.DeltaY = DeltaY;
            this.Delta = Delta;
        }

        public double GetEpsilon()
        {
            return this.Epsilon;
        }
        public double Getm()
        {
            return this.m;
        }
        public double GetBeta()
        {
            return this.Beta;
        }
        public double GetDelta()
        {
            return this.Delta;
        }
        public double GetDeltaT()
        {
            return this.DeltaT;
        }
        public double GetDeltaX()
        {
            return this.DeltaX;
        }
        public double GetDeltaY()
        {
            return this.DeltaY;
        }
    }
}
