using System;
using System.Collections.Generic;
using Microsoft.ML.Data;
using Salary.Infrastructure;
using Salary.Models;

namespace Salary.Services
{
    public static class Print
    {
        public static void Header(string value)
        {
            ConsoleHelper.WriteLines(new[]
            {
                "--------------------------------------------------",
                $"       {value}",
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
            ConsoleHelper.WriteLine($"       PredictedSalary:{prediction,10:N0}", ConsoleColor.Magenta);
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
        
        public static void PreviewData(IEnumerable<string> previewData)
        {
            ConsoleHelper.WriteLine("Preview Transformed Data", ConsoleColor.Cyan);
            ConsoleHelper.WithColor(ConsoleColor.Green, () =>
            {
                foreach (var line in previewData)
                {
                    Console.WriteLine(line);
                }
            });
        }

        public static void PaymentPredictions(List<PaymentSpikePrediction> predictions)
        {
            ConsoleHelper.WriteLine("Results", ConsoleColor.Yellow);

            foreach (var prediction in predictions)
            {
                var line = $"Alert: {prediction.IsSpike} | Payment: {prediction.Amount:N0} | P-Value: {prediction.PValue:N2}";

                ConsoleHelper.WriteLine(line, prediction.IsSpike ? ConsoleColor.Red : ConsoleColor.Green);
            }
        }
    }
}