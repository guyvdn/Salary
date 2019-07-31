using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.ML.Data;
using Salary.Domain;
using Salary.Infrastructure;
using Salary.Services.MachineLearning;

namespace Salary.Services
{
    public static class Print
    {
        public static void Header(string value)
        {
            ConsoleHelper.WriteLines(new[]
            {
                "--------------------------------------------------",
                $" {value}",
                "--------------------------------------------------"
            }, ConsoleColor.Yellow);
        }

        public static void Error(string message)
        {
            ConsoleHelper.WriteLine(message, ConsoleColor.Red);
        }

        public static void Employee(Employee employee)
        {
            ConsoleHelper.WriteLines(new[]
            {
                $"--------------------------------------------------",
                $"       Employee                                   ",
                $"--------------------------------------------------",
                $"       Age:            {employee.Age,10:N0}",
                $"       ExperienceLevel:{employee.ExperienceLevel,10}",
                $"       BaseSalary:     {employee.BaseSalary,10:N0}",
                $"       MinimumSalary:  {employee.MinimumSalary,10:N0}",
                $"       MaximumSalary:  {employee.MaximumSalary,10:N0}",
                $"--------------------------------------------------"
            }, ConsoleColor.Green);
        }

        public static void PredictedSalary(float prediction)
        {
            ConsoleHelper.WriteLine($"       PredictedSalary:{prediction,10:N2}", ConsoleColor.Magenta);
            ConsoleHelper.WriteLine("--------------------------------------------------", ConsoleColor.Green);
        }

        public static void Employees(IEnumerable<Employee> employees)
        {
            ConsoleHelper.WithColor(ConsoleColor.Green, () =>
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine($"Age: {employee.Age} | ExperienceLevel: {employee.ExperienceLevel} | Salary: {employee.Salary:N0}");
                }
            });
        }

        public static void Metrics(RegressionMetrics metrics)
        {
            ConsoleHelper.WriteLines(new[]
            {
                $"--------------------------------------------------",
                $"       Metrics                                    ",
                $"--------------------------------------------------",
                $"       LossFn:        {metrics.LossFunction,10:N2}",
                $"       R2 Score:      {metrics.RSquared,10:N2}",
                $"       Absolute loss: {metrics.MeanAbsoluteError,10:N2}",
                $"       Squared loss:  {metrics.MeanSquaredError,10:N2}",
                $"       RMS loss:      {metrics.RootMeanSquaredError,10:N2}",
                $"--------------------------------------------------"
            }, ConsoleColor.Green);
        }

        public static void PreviewData(ImmutableArray<DataDebuggerPreview.RowInfo> previewData, List<float[]> experienceLevelData, List<float[]> featuresData)
        {
            string SparseVector(IEnumerable<float> vector)
            {
                return "[" + string.Join(",", vector) + "]";
            }

            string GetValue(string key, object value, int i)
            {
                switch (key)
                {
                    case "LevelEncoded" when value is VBuffer<float>:
                        return SparseVector(experienceLevelData[i]);
                    case "Features":
                        return SparseVector(featuresData[i]);
                    default:
                        return value.ToString();
                }
            }

            ConsoleHelper.WriteLine("Preview Transformed Data", ConsoleColor.Cyan);
            ConsoleHelper.WithColor(ConsoleColor.Green, () =>
            {
                for (var i = 0; i < previewData.Length; i++)
                {
                    var data = new List<string>();
                    foreach (var (key, value) in previewData[i].Values)
                    {
                        data.Add($"{key}: {GetValue(key, value, i)}");
                    }
                    Console.WriteLine(string.Join(" | ", data));
                }
            });
        }

        public static void SpikePredictions(IEnumerable<PaymentValidation.SpikePrediction> predictions)
        {
            ConsoleHelper.WriteLine("Results", ConsoleColor.Yellow);

            foreach (var prediction in predictions)
            {
                var p = prediction.Prediction;
                var line = $"Alert: {p[0]} | Payment: {p[1]:N0} | P-Value: {p[2]:N2}";
                var isSpike = Math.Abs(p[0] - 1) < 0.001;

                ConsoleHelper.WriteLine(line, isSpike ? ConsoleColor.Red : ConsoleColor.Green);
            }
        }
    }
}