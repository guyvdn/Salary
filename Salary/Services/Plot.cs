using System.Collections.Generic;
using System.Linq;
using PLplot;
using Salary.Domain;

namespace Salary.Services
{
    public static class Plot
    {
        public static void RegressionChart(IList<KeyValuePair<Employee, float>> predictions, string fileName)
        {
            const int minValue = 1500;
            const int maxValue = 6500;
            const char dot = (char)22;
            const int red = 4;
            const int blue = 2;

            using (var pl = new PLStream())
            {
                pl.sdev("pngcairo");
                pl.sfnam(fileName);
                pl.spal0("cmap0_alternate.pal");
                pl.init();

                pl.env(minValue, maxValue, minValue, maxValue, AxesScale.Independent, AxisBox.BoxTicksLabelsAxes);

                pl.col0(blue);
                foreach (var (employee, prediction) in predictions)
                {
                    var x = employee.Salary;
                    var y = prediction;

                    pl.poin(new double[] { x }, new double[] { y }, dot);
                }
                
                var points = predictions.Select(p => ((float)p.Key.Salary, p.Value)).ToList();

                Calculate.RegressionLine(points, minValue, maxValue, out var y1, out var y2);

                pl.col0(red);
                pl.line(new double[] { minValue, maxValue }, new double[] { y1, y2 });
                pl.eop();
            }
        }
    }
}