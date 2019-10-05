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
        private Celda[][] bacterias;
        public int[][] contar()
        {
            int[][] vec = new int[bacterias.Length][];
            for (int i = 0; i < bacterias.Length; i++)
            {
                int[] subvec = new int[bacterias[i].Length];

                for (int j = 0; j < bacterias[i].Length; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            subvec[j] = bacterias[i][j + 1].siTrueTrue() + bacterias[i + 1][j + 1].siTrueTrue() + bacterias[i + 1][j].siTrueTrue();
                        }
                        else if (j == bacterias[i].Length - 1)
                        {
                            subvec[j] = bacterias[i][j - 1].siTrueTrue() + bacterias[i + 1][j - 1].siTrueTrue() + bacterias[i + 1][j].siTrueTrue();
                        }
                        else
                        {
                            subvec[j] = bacterias[i][j + 1].siTrueTrue() + bacterias[i][j - 1].siTrueTrue() + bacterias[i + 1][j + 1].siTrueTrue() + bacterias[i + 1][j].siTrueTrue() + bacterias[i + 1][j - 1].siTrueTrue();
                        }

                    }
                    else if (i == bacterias.Length - 1)
                    {
                        if (j == 0)
                        {
                            subvec[j] = bacterias[i][j + 1].siTrueTrue() + bacterias[i - 1][j + 1].siTrueTrue() + bacterias[i - 1][j].siTrueTrue();
                        }
                        else if (j == bacterias[i].Length - 1)
                        {
                            subvec[j] = bacterias[i][j - 1].siTrueTrue() + bacterias[i - 1][j - 1].siTrueTrue() + bacterias[i - 1][j].siTrueTrue();
                        }
                        else
                        {
                            subvec[j] = bacterias[i][j + 1].siTrueTrue() + bacterias[i][j - 1].siTrueTrue() + bacterias[i - 1][j + 1].siTrueTrue() + bacterias[i - 1][j].siTrueTrue() + bacterias[i - 1][j - 1].siTrueTrue();
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            subvec[j] = bacterias[i][j + 1].siTrueTrue() + bacterias[i + 1][j].siTrueTrue() + bacterias[i + 1][j + 1].siTrueTrue() + bacterias[i - 1][j + 1].siTrueTrue() + bacterias[i - 1][j].siTrueTrue();
                        }
                        else if (j == bacterias[i].Length - 1)
                        {
                            subvec[j] = bacterias[i][j - 1].siTrueTrue() + bacterias[i + 1][j].siTrueTrue() + bacterias[i + 1][j - 1].siTrueTrue() + bacterias[i - 1][j - 1].siTrueTrue() + bacterias[i - 1][j].siTrueTrue();
                        }
                        else
                        {
                            subvec[j] = bacterias[i][j + 1].siTrueTrue() + bacterias[i][j - 1].siTrueTrue() + bacterias[i + 1][j].siTrueTrue() + bacterias[i + 1][j + 1].siTrueTrue() + bacterias[i + 1][j - 1].siTrueTrue() + bacterias[i - 1][j + 1].siTrueTrue() + bacterias[i - 1][j - 1].siTrueTrue() + bacterias[i - 1][j].siTrueTrue();
                        }

                    }
                }

                vec[i] = subvec;
            }
            return vec;

        }
        public void SaveAndSet(int[][] vecinos)
        {
            memory.Push(bacterias);
            Celda[][] bacteriasgrid = new Celda[this.bacterias.Length][];
            for (int i = 0; i < this.bacterias.Length; i++)
            {
                Celda[] fila = new Celda[this.bacterias[i].Length];
                for (int j = 0; j < this.bacterias[i].Length; j++)
                {
                    this.bacterias[i][j].change(vecinos[i][j]);
                    fila[j] = new Celda(this.bacterias[i][j]);
                }
                bacteriasgrid[i] = fila;
            }
            this.bacterias = bacteriasgrid;
        }
        public void setBacterias(Celda[][] bacteriasgrid)
        {
            this.bacterias = bacteriasgrid;
        }
        public Stack<Celda[][]> getMemory()
        {
            return memory;
        }
        public Celda[][] getBacterias()
        {
            return this.bacterias;
        }
    }
}
