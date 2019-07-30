using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Salary.Domain;
using Salary.Infrastructure;

namespace Salary.Services.MachineLearning
{
    public class SalaryPrediction
    {
        private class EmployeeDto
        {
            public float Age { get; set; }
            public string Level { get; set; }
            public float Salary { get; set; }
        }

        private class Prediction
        {
            [ColumnName("Score")]
            public float Salary { get; set; }
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);

        public static ITransformer Train(IEnumerable<Employee> data)
        {
            var trainingData = MapData(data);

            var dataProcessPipeline = MlContext.Transforms.CopyColumns("Label", nameof(EmployeeDto.Salary))
                .Append(MlContext.Transforms.Categorical.OneHotEncoding("LevelEncoded", nameof(EmployeeDto.Level)))
                .Append(MlContext.Transforms.Concatenate("Features", nameof(EmployeeDto.Age), "LevelEncoded"));

            var trainingDataView = MlContext.Data.LoadFromEnumerable(trainingData);
            PreviewData(dataProcessPipeline, trainingDataView);

            var trainer = MlContext.Regression.Trainers.Sdca("Label", "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return ConsoleSpinner.Execute("Training", () => trainingPipeline.Fit(trainingDataView));
        }

        public static void Evaluate(ITransformer trainedModel, IEnumerable<Employee> data)
        {
            var evaluationData = MapData(data);

            var metrics = ConsoleSpinner.Execute("Evaluating", () =>
            {
                var validationDataView = MlContext.Data.LoadFromEnumerable(evaluationData);
                var predictions = trainedModel.Transform(validationDataView);
                return MlContext.Regression.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score");
            });

            Print.Metrics(metrics);
        }

        public static float GetPrediction(Employee employee)
        {
            var predictionEngine = MlContext.Model.CreatePredictionEngine<EmployeeDto, Prediction>(Program.TrainedModel);
            var prediction = predictionEngine.Predict(new EmployeeDto { Age = employee.Age, Level = employee.ExperienceLevel.ToString() });
            return prediction.Salary;
        }

        private static ICollection<EmployeeDto> MapData(IEnumerable<Employee> data)
        {
            return data.Select(x => new EmployeeDto
            {
                Age = x.Age,
                Level = x.ExperienceLevel.ToString(),
                Salary = x.Salary
            }).ToList();
        }

        private static void PreviewData<T>(EstimatorChain<T> pipeline, IDataView dataView, int maxRows = 10) where T : class, ITransformer
        {
            var transformer = pipeline.Fit(dataView);
            var transformedData = transformer.Transform(dataView);
            var previewData = transformedData.Preview(maxRows);
            var experienceLevelData = transformedData.GetColumn<float[]>("LevelEncoded").Take(maxRows).ToList();
            var featuresData = transformedData.GetColumn<float[]>("Features").Take(maxRows).ToList();

            Print.PreviewData(previewData.RowView, experienceLevelData, featuresData);
        }
    }
}