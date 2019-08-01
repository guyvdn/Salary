﻿using System.Collections.Generic;
using System.Linq;
using Salary.Domain;
using Salary.Infrastructure;
using Salary.Services;
using Salary.Services.MachineLearning;

namespace Salary.Commands
{
    public static class TrainModel
    {
        public static void Execute()
        {
            Print.Header("Train Model");

            if (!Validate.DataIsLoaded()) return;

            SplitData(Program.Data, out var trainingData, out var validationData);

            IEnumerable<string> previewData = null;
            Program.TrainedModel = ConsoleSpinner.Execute("Training", () => SalaryPredictionService.Train(trainingData, out previewData));
            Print.PreviewData(previewData);

            var metrics = ConsoleSpinner.Execute("Evaluating", () => SalaryPredictionService.Evaluate(Program.TrainedModel, validationData));
            Print.Metrics(metrics);
        }

        private static void SplitData(ICollection<Employee> data, out List<Employee> trainingData, out List<Employee> validationData)
        {
            trainingData = data.Take(data.Count / 2).ToList();
            validationData = data.Skip(data.Count / 2).ToList();
        }
    }
}