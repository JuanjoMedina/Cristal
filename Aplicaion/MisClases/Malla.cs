using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MisClases
{
    [Serializable]
    public class Malla
    {
        private Stack<Celda[][]> memory=new Stack<Celda[][]>();
        private Celda[][] celdas;

        public void Calcular(bool espejo, Variables var)
        {
            for (int i = 1; i < celdas.Length-1; i++)
            {
                for (int j = 1; j < celdas[i].Length-1; j++)
                {
                    double Phase = ExtensionClass.GetPhase(var, celdas[i][j].getPhase(), celdas[i - 1][j].getPhase(), celdas[i + 1][j].getPhase(), celdas[i][j + 1].getPhase(), celdas[i][j - 1].getPhase(),celdas[i][j].getTemperature());
                    double Temp = ExtensionClass.GetTemp(var, celdas[i][j].getPhase(), celdas[i+1][j].getTemperature(),celdas[i][j].getTemperature(),celdas[i-1][j].getTemperature(),celdas[i][j+1].getTemperature(),celdas[i][j-1].getTemperature(),Phase);
                    this.celdas[i][j].setFuturePhase(Phase);
                    this.celdas[i][j].setFutureTemperature(Temp);
                    
                }
            }

            if (espejo)
            {
                for (int i=0; i < celdas.Length; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 1; j < celdas[i].Length-1; j++)
                        {
                            this.celdas[i][j].setFutureTemperature(this.celdas[i + 1][j].getFutureTemperature());
                            this.celdas[i][j].setFuturePhase(this.celdas[i + 1][j].getFuturePhase());
                        }
                        this.celdas[i][0].setFutureTemperature(this.celdas[1][1].getFutureTemperature());
                        this.celdas[i][0].setFuturePhase(this.celdas[1][1].getFuturePhase());
                        this.celdas[i][celdas[i].Length-1].setFutureTemperature(this.celdas[1][celdas[i].Length - 1].getFutureTemperature());
                        this.celdas[i][celdas[i].Length-1].setFuturePhase(this.celdas[1][celdas[i].Length - 1].getFuturePhase());
                    }
                    else if (i == celdas.Length)
                    {
                        for (int j = 1; j < celdas[i].Length - 1; j++)
                        {
                            this.celdas[i][j].setFutureTemperature(this.celdas[i -1][j].getFutureTemperature());
                            this.celdas[i][j].setFuturePhase(this.celdas[i -1][j].getFuturePhase());
                        }
                        this.celdas[i][0].setFutureTemperature(this.celdas[i-1][1].getFutureTemperature());
                        this.celdas[i][0].setFuturePhase(this.celdas[i-1][1].getFuturePhase());
                        this.celdas[i][celdas[i].Length-1].setFutureTemperature(this.celdas[i-1][celdas[i].Length - 1].getFutureTemperature());
                        this.celdas[i][celdas[i].Length-1].setFuturePhase(this.celdas[i-1][celdas[i].Length - 1].getFuturePhase());
                    }
                    else
                    {
                        this.celdas[i][0].setFutureTemperature(this.celdas[i][1].getFutureTemperature());
                        this.celdas[i][0].setFuturePhase(this.celdas[i][1].getFuturePhase());
                        this.celdas[i][celdas[i].Length-1].setFutureTemperature(this.celdas[i][celdas[i].Length-1].getFutureTemperature());
                        this.celdas[i][celdas[i].Length-1].setFuturePhase(this.celdas[i][celdas[i].Length-1].getFuturePhase());
                    }
                }

            }
        }

        public void SaveAndSet()
        {
            memory.Push(celdas);
            Celda[][] celdasgrid = new Celda[this.celdas.Length][];
            for (int i = 0; i < this.celdas.Length; i++)
            {
                Celda[] fila = new Celda[this.celdas[i].Length];
                for (int j = 0; j < this.celdas[i].Length; j++)
                {
                    this.celdas[i][j].change();
                    fila[j] = new Celda(this.celdas[i][j]);
                }
                celdasgrid[i] = fila;
            }
            this.celdas = celdasgrid;
        }


        public void setceldas(Celda[][] celdasgrid)
        {
            this.celdas = celdasgrid;
        }
        public Stack<Celda[][]> getMemory()
        {
            return memory;
        }
        public Celda[][] getceldas()
        {
            return this.celdas;
        }
    }
}
