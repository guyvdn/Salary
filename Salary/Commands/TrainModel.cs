using System.Collections.Generic;
using System.Linq;
using Salary.Domain;
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
            Program.TrainedModel = SalaryPrediction.Train(trainingData);
            SalaryPrediction.Evaluate(Program.TrainedModel, validationData);
        }

        private static void SplitData(ICollection<Employee> data, out List<Employee> trainingData, out List<Employee> validationData)
        {
            trainingData = data.Take(data.Count / 2).ToList();
            validationData = data.Skip(data.Count / 2).ToList();
        }
    }
}