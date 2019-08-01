using System.Linq;
using Salary.Services;
using Salary.Services.MachineLearning;

namespace Salary.Commands
{
    public static class PlotRegressionChart
    {
        public static void Execute()
        {
            Print.Header("Plot Regression Chart");

            if (!Validate.DataIsLoaded() || !Validate.ModelIsTrained()) return;

            var predictions = SalaryPredictionService.GetPrediction(Program.Data.Take(100)).ToList();

            const string fileName = "chart.png";
            Plot.RegressionChart(predictions, fileName);
            Plot.ShowImage(fileName);
        }
    }
}