using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MisClases
{
    public static class ExtensionClass
    {      
       
        public static void Represent(this DataGridView dataGridView,Celda[][] bacterias)
        {
            for (int i = 0; i < bacterias.Length; i++)
            {
                for (int j = 0; j < bacterias[i].Length; j++)
                {
                    Celda bacteria = bacterias[i][j];
                    if (bacteria.getViva())
                    {
                        dataGridView.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Red;
                        dataGridView.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        dataGridView.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                        dataGridView.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.White;
                    }
                }
            }
        }
    }
}
