using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    [Serializable]
    public class Guardado
    {
        Malla grid;
        Variables variables;
        bool mirror;
        List<double> Tiempo;
        public Guardado(Malla grid,Variables var, bool mirror, List<double> Tiempo)
        {
            this.grid = grid;
            this.variables = var;
            this.mirror = mirror;
            this.Tiempo = Tiempo;
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
    }
}
