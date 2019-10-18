using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
using System.Drawing;

namespace MisClases
{
    public class Charts
    {
        public void CreateGraph(ZedGraphControl zgc, PointPairList list1, String what)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;
            LineItem myCurve;

            // Generate a red curve with circle
            if (what == "Temperature")
            {
                myCurve = myPane.AddCurve(what, list1, Color.Red, SymbolType.Diamond);
                myPane.YAxis.Title.Text = "Normalized Temp";
                myPane.XAxis.Title.Text = "Time (s)";
            }
            else
            {
                myCurve = myPane.AddCurve(what, list1, Color.Green, SymbolType.Diamond);
                myPane.YAxis.Title.Text = "Normalized Phase";
                myPane.XAxis.Title.Text = "Time (s)";
            }
            myPane.Title.IsVisible = false;
            myCurve.Label.IsVisible = false;
            zgc.IsEnableWheelZoom = false;
            zgc.IsEnableHPan = false;
            zgc.IsEnableZoom = false;


            // Tell ZedGraph to refigure the axes since the data have changed
            zgc.AxisChange();
        }

    }
}
