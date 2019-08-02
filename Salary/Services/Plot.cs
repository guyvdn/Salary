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
        private const int Green = 9;

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

        public static void RegressionChart(IList<Employee> employees, string fileName)
        {
            const int minValueX = 20;
            const int maxValueX = 60;
            const int minValueY = 1500;
            const int maxValueY = 6500;
            
            PlotToFile(fileName, pl =>
            {
                pl.env(minValueX, maxValueX, minValueY, maxValueY, AxesScale.Independent, AxisBox.BoxTicksLabelsAxes);
                pl.lab("Age", "Salary", "Regression of Salary");

                void Plot(ExperienceLevel experienceLevel, int color)
                {
                    var list = employees.Where(e => e.ExperienceLevel == experienceLevel).Take(100);
                    var points = list.Select(employee => ((float) employee.Age, (float) employee.Salary)).ToList();

                    pl.col0(color);
                    points.ForEach(p => pl.poin(new double[] {p.Item1}, new double[] {p.Item2}, Dot));

                    pl.col0(color);
                    StatisticsService.CalculateRegressionLine(points, minValueX, maxValueX, out var y1, out var y2);
                    pl.line(new double[] {minValueX, maxValueX}, new double[] {y1, y2});
                }

                Plot(ExperienceLevel.Junior, Green);
                Plot(ExperienceLevel.Medior, Blue);
                Plot(ExperienceLevel.Senior, Red);
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