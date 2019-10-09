using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    public class Variables
    {
        double Epsilon;
        double m;
        double Beta;
        double Delta;
        double DeltaT;
        double DeltaX;
        double DeltaY;

        public Variables(double Epsilon, double m, double Beta, double DeltaT, double DeltaX, double DeltaY)
        {
            this.Epsilon = Epsilon;
            this.m = m;
            this.Beta = Beta;
            this.DeltaT = DeltaT;
            this.DeltaX = DeltaX;
            this.DeltaY = DeltaY;
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
