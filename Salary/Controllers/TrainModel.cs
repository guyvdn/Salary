using System.Collections.Generic;
using Salary.Infrastructure;
using Salary.MachineLearning;
using Salary.Services;

namespace Salary.Controllers
{
    public static class TrainModel
    {
        public static void Execute()
        {
            Print.Header("Train Model");

            if (!Validate.DataIsLoaded()) return;

            IEnumerable<string> previewData = null;

            Program.TrainedModel = ConsoleSpinner.Execute("Training", () =>
                SalaryPredictionService.Train(Program.TrainingData, out previewData)
            );

            Print.PreviewTransformedData(previewData);

            var metrics = ConsoleSpinner.Execute("Evaluating", () =>
                SalaryPredictionService.Evaluate(Program.TrainedModel, Program.TestData)
            );

            Print.Metrics(metrics);
        }
    }
}