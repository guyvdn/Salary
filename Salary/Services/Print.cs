using System;
using System.Collections.Generic;
using Microsoft.ML.Data;
using Salary.Extensions;
using Salary.Infrastructure;
using Salary.Models;

namespace Salary.Services
{
    public static class Print
    {
        private static readonly string HorizontalRule = new string('-', Console.WindowWidth - 1);
        private static readonly string Tab = new string(' ', 4);

        public static void Header(string value, ConsoleColor color = ConsoleColor.Yellow)
        {
            ConsoleHelper.WriteLines(new[]
            {
                HorizontalRule,
                Tab + value,
                HorizontalRule
            }, color);
        }

        private static void Footer(ConsoleColor color)
        {
            ConsoleHelper.WriteLine(HorizontalRule, color);
        }

        public static void Error(string message)
        {
            ConsoleHelper.WriteLine(message, ConsoleColor.Red);
        }

        public static void Employee(Employee employee)
        {
            Header("Employee", ConsoleColor.Green);
            ConsoleHelper.WriteLines(new[]
            {
                $"{Tab}Age:            {employee.Age,10:N0}",
                $"{Tab}ExperienceLevel:{employee.ExperienceLevel,10}",
                $"{Tab}BaseSalary:     {employee.BaseSalary,10:N0}",
                $"{Tab}MinimumSalary:  {employee.MinimumSalary,10:N0}",
                $"{Tab}MaximumSalary:  {employee.MaximumSalary,10:N0}",
            }, ConsoleColor.Green);
            Footer(ConsoleColor.Green);
        }

        public static void PredictedSalary(float prediction)
        {
            ConsoleHelper.WriteLine($"{Tab}PredictedSalary:{prediction,10:N0}", ConsoleColor.Magenta);
            Footer(ConsoleColor.Green);
        }

        public static void PreviewGeneratedData(IEnumerable<Employee> employees)
        {
            Header("Preview generated data", ConsoleColor.Green);
            employees.ForEach(e => ConsoleHelper.WriteLine($" Age: {e.Age} | ExperienceLevel: {e.ExperienceLevel} | Salary: {e.Salary:N0}", ConsoleColor.Green));
            Footer(ConsoleColor.Green);
        }

        public static void PreviewTransformedData(IEnumerable<string> previewData)
        {
            Header("Preview Transformed Data", ConsoleColor.Green);
            previewData.ForEach(line => ConsoleHelper.WriteLine(" " + line, ConsoleColor.Green));
            ConsoleHelper.WriteLine(HorizontalRule, ConsoleColor.Green);
        }

        public static void Metrics(RegressionMetrics metrics)
        {
            Header("Metrics", ConsoleColor.Green);
            ConsoleHelper.WriteLines(new[]
            {
                $"{Tab}R-Squared (Coefficient of determination): {metrics.RSquared,10:N2}",
                $"{Tab}Absolute loss (Mean absolute error):      {metrics.MeanAbsoluteError,10:N2}",
                $"{Tab}Squared loss (Mean Squared Error):        {metrics.MeanSquaredError,10:N2}",
                $"{Tab}RMS loss (Root Mean Squared Error):       {metrics.RootMeanSquaredError,10:N2}"
            }, ConsoleColor.Green);
            Footer(ConsoleColor.Green);
        }

        public static void PaymentPredictions(IEnumerable<PaymentSpikePrediction> predictions)
        {
            Header("Results", ConsoleColor.Green);
            foreach (var prediction in predictions)
            {
                var line = $" Alert: {prediction.IsSpike,-5} | Payment: {prediction.Amount:N0} | P-Value: {prediction.PValue:N2}";
                ConsoleHelper.WriteLine(line, prediction.IsSpike ? ConsoleColor.Red : ConsoleColor.Green);
            }
            Footer(ConsoleColor.Green);
        }
    }
}