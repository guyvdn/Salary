using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Salary.Domain;

namespace Salary.Services.MachineLearning
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
            [ColumnName("Score")]
            public float Salary { get; set; }
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);

        public static ITransformer Train(IEnumerable<Employee> data, out IEnumerable<string> previewData)
        {
            var trainingData = data.Select(employee => new EmployeeDto(employee));

            var dataProcessPipeline = MlContext.Transforms.CopyColumns("Label", nameof(EmployeeDto.Salary))
                .Append(MlContext.Transforms.Categorical.OneHotEncoding("LevelEncoded", nameof(EmployeeDto.Level)))
                .Append(MlContext.Transforms.Concatenate("Features", nameof(EmployeeDto.Age), "LevelEncoded"));

            var trainingDataView = MlContext.Data.LoadFromEnumerable(trainingData);
            previewData = PreviewDataService.GetPreviewData(dataProcessPipeline, trainingDataView);

            var trainer = MlContext.Regression.Trainers.Sdca("Label", "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline.Fit(trainingDataView);
        }

        public static RegressionMetrics Evaluate(ITransformer trainedModel, IEnumerable<Employee> data)
        {
            var evaluationData = data.Select(employee => new EmployeeDto(employee));
            var validationDataView = MlContext.Data.LoadFromEnumerable(evaluationData);
            var predictions = trainedModel.Transform(validationDataView);
            return MlContext.Regression.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score");
        }

        private static float GetPrediction(PredictionEngineBase<EmployeeDto, SalaryPrediction> predictionEngine, Employee employee)
        {
            return predictionEngine.Predict(new EmployeeDto(employee)).Salary;
        }

        public static float GetPrediction(Employee employee)
        {
            var predictionEngine = MlContext.Model.CreatePredictionEngine<EmployeeDto, SalaryPrediction>(Program.TrainedModel);
            return GetPrediction(predictionEngine, employee);
        }

        public static IEnumerable<KeyValuePair<Employee, float>> GetPrediction(IEnumerable<Employee> employees)
        {
            var predictionEngine = MlContext.Model.CreatePredictionEngine<EmployeeDto, SalaryPrediction>(Program.TrainedModel);
            return employees.Select(e => new KeyValuePair<Employee, float>(e, GetPrediction(predictionEngine, e)));
        }
    }
}