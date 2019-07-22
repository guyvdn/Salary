using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using PredictSalary.Domain;

namespace PredictSalary.Commands
{
    public class MachineLearning
    {
        private class EmployeeDto
        {
            public float Age { get; set; }
            public string ExperienceLevel { get; set; }
            public float Salary { get; set; }
            public float Score { get; set; }
        }

        private class SalaryPrediction
        {
            [ColumnName("Score")]
            public float Salary;
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);
        private static TransformerChain<RegressionPredictionTransformer<LinearRegressionModelParameters>> trainedModel;

        public static void Learn()
        {
            if (!Program.Data.Any())
            {
                Console.WriteLine("No data has been loaded yet");
                return;
            }

            var data = Program.Data.Select(x => new EmployeeDto { Age = x.Age, ExperienceLevel = x.ExperienceLevel.ToString(), Salary = x.Salary }).ToList();
            var trainingData = data.Take(data.Count / 2).ToList();
            var validationData = data.Skip(data.Count / 2).ToList();

            var trainingDataView = MlContext.Data.LoadFromEnumerable(trainingData);
            var validationDataView = MlContext.Data.LoadFromEnumerable(validationData);

            var dataProcessPipeline = MlContext.Transforms.CopyColumns("Label", nameof(EmployeeDto.Salary))
                .Append(MlContext.Transforms.Concatenate("Features", nameof(EmployeeDto.Age)));
            //.CopyColumns("Label", nameof(EmployeeDto.Salary))
            //.Append(MlContext.Transforms.Categorical.OneHotEncoding(nameof(EmployeeDto.ExperienceLevel)))
            //.Append(MlContext.Transforms.NormalizeMeanVariance(nameof(EmployeeDto.Age)))
            //.Append(MlContext.Transforms.Text.FeaturizeText("ExperienceLevelEncoded", nameof(EmployeeDto.ExperienceLevel)))
            //.Append(MlContext.Transforms.Concatenate("Features", nameof(EmployeeDto.Age)));


            PreviewFeaturesData(dataProcessPipeline, trainingDataView);

            var trainer = MlContext.Regression.Trainers.Sdca("Label", "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            Console.WriteLine("Training");
            trainedModel = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine("Evaluating");
            var predictions = trainedModel.Transform(validationDataView);
            var metrics = MlContext.Regression.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Metrics                                  ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       LossFn:        {metrics.LossFunction:0.##}");
            Console.WriteLine($"*       R2 Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Absolute loss: {metrics.MeanAbsoluteError:#.##}");
            Console.WriteLine($"*       Squared loss:  {metrics.MeanSquaredError:#.##}");
            Console.WriteLine($"*       RMS loss:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*************************************************");
        }

        private static void PreviewFeaturesData<T>(EstimatorChain<T> pipeline, IDataView dataView) where T : class, ITransformer
        {
            var transformer = pipeline.Fit(dataView);
            var transformedData = transformer.Transform(dataView);

            Console.WriteLine("Preview Transformed Data");
            var previewTransformedData = transformedData.Preview(10);
            foreach (var row in previewTransformedData.RowView)
            {
                var ColumnCollection = row.Values;
                string lineToPrint = "Row--> ";
                foreach (KeyValuePair<string, object> column in ColumnCollection)
                {
                    lineToPrint += $"| {column.Key}:{column.Value}";
                }
                Console.WriteLine(lineToPrint + "\n");
            }

            Console.WriteLine("Preview Features Data");
            var features = transformedData.GetColumn<float[]>("Features").Take(10).ToList();
            features.ForEach(row => { Console.WriteLine(string.Join("", row)); });
        }

        public static float GetPrediction(Employee employee)
        {
            if (trainedModel == null)
            {
                Console.WriteLine("Machine has not been trained yet");
                return 0;
            }

            var predictionEngine = MlContext.Model.CreatePredictionEngine<EmployeeDto, SalaryPrediction>(trainedModel);
            var prediction = predictionEngine.Predict(new EmployeeDto { Age = employee.Age, ExperienceLevel = employee.ExperienceLevel.ToString() });
            return prediction.Salary;
        }
    }
}