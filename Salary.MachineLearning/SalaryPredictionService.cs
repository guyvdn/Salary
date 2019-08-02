using Microsoft.ML;
using Microsoft.ML.Data;
using Salary.Models;
using System.Collections.Generic;
using System.Linq;

namespace Salary.MachineLearning
{
    public class SalaryPredictionService
    {
        private class EmployeeDto
        {
            public float Age { get; }
            public string Level { get; }
            public float Salary { get; }

            public EmployeeDto(Employee employee)
            {
                Age = employee.Age;
                Level = employee.ExperienceLevel.ToString();
                Salary = employee.Salary;
            }
        }

        private class SalaryPrediction
        {
            [ColumnName(ScoreColumnName)]
            public float Salary { get; set; }
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);
        private const string ScoreColumnName = "Score";
        private const string LabelColumnName = "Label";
        private const string FeatureColumnName = "Features";

        public static ITransformer Train(IEnumerable<Employee> data, out IEnumerable<string> previewData)
        {
            var trainingData = data.Select(employee => new EmployeeDto(employee));

            var dataProcessPipeline = MlContext.Transforms.CopyColumns(LabelColumnName, nameof(EmployeeDto.Salary))
                .Append(MlContext.Transforms.Categorical.OneHotEncoding($"{nameof(EmployeeDto.Level)}Encoded", nameof(EmployeeDto.Level)))
                .Append(MlContext.Transforms.Concatenate(FeatureColumnName, nameof(EmployeeDto.Age), $"{nameof(EmployeeDto.Level)}Encoded"));

            var trainingDataView = MlContext.Data.LoadFromEnumerable(trainingData);
            previewData = PreviewDataService.GetPreviewData(dataProcessPipeline, trainingDataView);

            var trainer = MlContext.Regression.Trainers.Sdca(LabelColumnName, FeatureColumnName);
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline.Fit(trainingDataView);
        }

        public static RegressionMetrics Evaluate(ITransformer trainedModel, IEnumerable<Employee> data)
        {
            var evaluationData = data.Select(employee => new EmployeeDto(employee));
            var validationDataView = MlContext.Data.LoadFromEnumerable(evaluationData);
            var predictions = trainedModel.Transform(validationDataView);
            return MlContext.Regression.Evaluate(predictions, labelColumnName: LabelColumnName, scoreColumnName: ScoreColumnName);
        }

        public static float GetPrediction(ITransformer trainedModel, Employee employee)
        {
            var predictionEngine = MlContext.Model.CreatePredictionEngine<EmployeeDto, SalaryPrediction>(trainedModel);
            return predictionEngine.Predict(new EmployeeDto(employee)).Salary;
        }
    }
}