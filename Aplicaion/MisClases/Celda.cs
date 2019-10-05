using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisClases
{
    [Serializable]
    public class Celda
    {

        private string nombre;
        private bool[][] reglas;
        private bool viva;
        private bool vivaFuturo;
        
        public Celda()
        {
            this.nombre = null;
            this.reglas = null;
            this.viva = false;
            this.vivaFuturo = false;
        }
        public Celda(string nombre,bool[][] rules, bool alive)
        {
            this.nombre = nombre;
            this.reglas = rules;
            this.viva = alive;
            this.vivaFuturo = false;
        }
        public Celda(Celda bacteria)
        {
            this.nombre = bacteria.nombre;
            this.reglas = bacteria.reglas;
            this.viva = bacteria.vivaFuturo;
            this.vivaFuturo = false;
        }

        public string getName()
        {
            return this.nombre;
        }
        public bool getViva()
        {
            return this.viva;
        }
        public bool[][] getReglas()
        {
            return this.reglas;
        }
        public void setInitialViva()
        {
            if (this.viva)
                this.viva = false;
            else
                this.viva = true;
        }
        public int siTrueTrue()
        {
            if (this.viva)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void change(int vecinos)
        {
            int i;
            if (this.viva)
                i = 0;
            else
                i = 1;
            if (this.reglas[vecinos][i])
                this.vivaFuturo = true;
            else
                this.vivaFuturo = false;

        }
    }
}
