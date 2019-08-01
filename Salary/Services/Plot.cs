using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PLplot;
using Salary.MachineLearning;
using Salary.Models;

namespace Salary.Services
{
    public static class Plot
    {
        private const char Dot = (char)20;
        private const int Red = 4;
        private const int Blue = 2;

        private static void PlotToFile(string fileName, Action<PLStream> action)
        {
            using (var pl = new PLStream())
            {
                pl.sdev("pngcairo");
                pl.sfnam(fileName);
                pl.spal0("cmap0_alternate.pal");
                pl.init();

                action(pl);

                pl.eop();
            }
        }

        public static void RegressionChart(IList<KeyValuePair<Employee, float>> predictions, string fileName)
        {
            const int minValue = 1500;
            const int maxValue = 6500;

            PlotToFile(fileName, pl =>
            {
                pl.env(minValue, maxValue, minValue, maxValue, AxesScale.Equal, AxisBox.BoxTicksLabelsAxes);
                pl.lab("Actual", "Predicted", "Distribution of Salary Prediction");

                pl.col0(Blue);
                foreach (var (employee, prediction) in predictions)
                {
                    var x = employee.Salary;
                    var y = prediction;

                    pl.poin(new double[] { x }, new double[] { y }, Dot);
                }

                var points = predictions.Select(p => ((float)p.Key.Salary, p.Value)).ToList();

                StatisticsService.CalculateRegressionLine(points, minValue, maxValue, out var y1, out var y2);

                pl.col0(Red);
                pl.line(new double[] { minValue, maxValue }, new double[] { y1, y2 });
            });
        }

        public static void PaymentChart(List<PaymentSpikePrediction> payments, string fileName)
        {
            const int xMin = 0;
            var xMax = payments.Count;
            var yMin = payments.Min(x => x.Amount) - 200;
            var yMax = payments.Max(x => x.Amount) + 200;

            PlotToFile(fileName, pl =>
            {
                pl.env(xMin, xMax, yMin, yMax, AxesScale.Independent, AxisBox.BoxTicksLabelsAxes);
                pl.lab("Time", "Amount", "Payments overview");

                for (var i = 0; i < payments.Count - 1; i++)
                {
                    pl.col0(payments[i + 1].IsSpike ? Red : Blue);
                    pl.line(new double[] { i, i + 1 }, new double[] { payments[i].Amount, payments[i + 1].Amount });
                }
            });
        }

        public static void ShowImage(string fileName)
        {
            new Process { StartInfo = new ProcessStartInfo(@".\" + fileName) { UseShellExecute = true } }.Start();
        }
    }
}