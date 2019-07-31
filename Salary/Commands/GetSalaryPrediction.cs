using System;
using System.Diagnostics;
using System.Linq;
using Salary.Domain;
using Salary.Infrastructure;
using Salary.Services;
using Salary.Services.MachineLearning;

namespace Salary.Commands
{
    public static class GetSalaryPrediction
    {
        public static void Execute()
        {
            Print.Header("Get Salary Prediction");

            if (!Validate.DataIsLoaded() || !Validate.ModelIsTrained()) return;

            var age = ConsoleHelper.GetNumber("Enter Age of Employee:");

            ConsoleHelper.WriteLine("Select ExperienceLevel of Employee", ConsoleColor.Cyan);
            var experienceLevel = ConsoleHelper.PickOption(ExperienceLevel.Values);

            var employee = new Employee(age, experienceLevel, 0);
            Print.Employee(employee);

            var prediction = SalaryPrediction.GetPrediction(employee);
            Print.PredictedSalary(prediction);
        }

        public static void RegressionChart()
        {
            const string fileName = "chart.png";

            Print.Header("Get Salary Prediction chart");

            if (!Validate.DataIsLoaded() || !Validate.ModelIsTrained()) return;

            var predictions = SalaryPrediction.GetPrediction(Program.Data.Take(100)).ToList();

            Plot.RegressionChart(predictions, fileName);

            ShowImage(fileName);
        }

        private static void ShowImage(string fileName)
        {
            new Process {StartInfo = new ProcessStartInfo(@".\" + fileName) {UseShellExecute = true}}.Start();
        }
    }
}