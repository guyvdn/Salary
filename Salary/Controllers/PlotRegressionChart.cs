using Salary.Services;

namespace Salary.Controllers
{
    public static class PlotRegressionChart
    {
        public static void Execute()
        {
            Print.Header("Plot Regression Chart");

            if (!Validate.DataIsLoaded()) return;

            const string fileName = "chart.png";

            Plot.RegressionChart(Program.TrainingData, fileName);

            Plot.ShowImage(fileName);
        }
    }
}