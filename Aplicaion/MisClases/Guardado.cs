using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace MisClases
{
    [Serializable]
    public class Guardado
    {
        Malla grid;
        Variables variables;
        bool mirror;
        List<double> Tiempo;
        PointPairList TempValues;
        PointPairList PhaseValues;

        public Guardado(Malla grid,Variables var, bool mirror, List<double> Tiempo, PointPairList temp, PointPairList phase)
        {
            this.grid = grid;
            this.variables = var;
            this.mirror = mirror;
            this.Tiempo = Tiempo;
            this.TempValues = temp;
            this.PhaseValues = phase;
        }
        public Malla getGrid()
        {
            return this.grid;
        }
        public Variables getVariables()
        {
            return this.variables;
        }
        public bool getMirror()
        {
            return this.mirror;
        }
        public List<double> getTiempo()
        {
            return Tiempo;
        }
        public PointPairList getTemp()
        {
            return TempValues;
        }
        public PointPairList getPhase()
        {
            return PhaseValues;
        }
    }
}
