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
        //public int[][] contar()
        //{
        //    int[][] vec = new int[celdas.Length][];
        //    for (int i = 0; i < celdas.Length; i++)
        //    {
        //        int[] subvec = new int[celdas[i].Length];

        //        for (int j = 0; j < celdas[i].Length; j++)
        //        {
        //            if (i == 0)
        //            {
        //                if (j == 0)
        //                {
        //                    subvec[j] = celdas[i][j + 1].siTrueTrue() + celdas[i + 1][j + 1].siTrueTrue() + celdas[i + 1][j].siTrueTrue();
        //                }
        //                else if (j == celdas[i].Length - 1)
        //                {
        //                    subvec[j] = celdas[i][j - 1].siTrueTrue() + celdas[i + 1][j - 1].siTrueTrue() + celdas[i + 1][j].siTrueTrue();
        //                }
        //                else
        //                {
        //                    subvec[j] = celdas[i][j + 1].siTrueTrue() + celdas[i][j - 1].siTrueTrue() + celdas[i + 1][j + 1].siTrueTrue() + celdas[i + 1][j].siTrueTrue() + celdas[i + 1][j - 1].siTrueTrue();
        //                }

        //            }
        //            else if (i == celdas.Length - 1)
        //            {
        //                if (j == 0)
        //                {
        //                    subvec[j] = celdas[i][j + 1].siTrueTrue() + celdas[i - 1][j + 1].siTrueTrue() + celdas[i - 1][j].siTrueTrue();
        //                }
        //                else if (j == celdas[i].Length - 1)
        //                {
        //                    subvec[j] = celdas[i][j - 1].siTrueTrue() + celdas[i - 1][j - 1].siTrueTrue() + celdas[i - 1][j].siTrueTrue();
        //                }
        //                else
        //                {
        //                    subvec[j] = celdas[i][j + 1].siTrueTrue() + celdas[i][j - 1].siTrueTrue() + celdas[i - 1][j + 1].siTrueTrue() + celdas[i - 1][j].siTrueTrue() + celdas[i - 1][j - 1].siTrueTrue();
        //                }
        //            }
        //            else
        //            {
        //                if (j == 0)
        //                {
        //                    subvec[j] = celdas[i][j + 1].siTrueTrue() + celdas[i + 1][j].siTrueTrue() + celdas[i + 1][j + 1].siTrueTrue() + celdas[i - 1][j + 1].siTrueTrue() + celdas[i - 1][j].siTrueTrue();
        //                }
        //                else if (j == celdas[i].Length - 1)
        //                {
        //                    subvec[j] = celdas[i][j - 1].siTrueTrue() + celdas[i + 1][j].siTrueTrue() + celdas[i + 1][j - 1].siTrueTrue() + celdas[i - 1][j - 1].siTrueTrue() + celdas[i - 1][j].siTrueTrue();
        //                }
        //                else
        //                {
        //                    subvec[j] = celdas[i][j + 1].siTrueTrue() + celdas[i][j - 1].siTrueTrue() + celdas[i + 1][j].siTrueTrue() + celdas[i + 1][j + 1].siTrueTrue() + celdas[i + 1][j - 1].siTrueTrue() + celdas[i - 1][j + 1].siTrueTrue() + celdas[i - 1][j - 1].siTrueTrue() + celdas[i - 1][j].siTrueTrue();
        //                }

        //            }
        //        }

        //        vec[i] = subvec;
        //    }
        //    return vec;

        //}
        //public void SaveAndSet(int[][] vecinos)
        //{
        //    memory.Push(celdas);
        //    Celda[][] celdasgrid = new Celda[this.celdas.Length][];
        //    for (int i = 0; i < this.celdas.Length; i++)
        //    {
        //        Celda[] fila = new Celda[this.celdas[i].Length];
        //        for (int j = 0; j < this.celdas[i].Length; j++)
        //        {
        //            this.celdas[i][j].change(vecinos[i][j]);
        //            fila[j] = new Celda(this.celdas[i][j]);
        //        }
        //        celdasgrid[i] = fila;
        //    }
        //    this.celdas = celdasgrid;
        //}
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
