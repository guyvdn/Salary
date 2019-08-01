using System;
using Salary.Infrastructure;
using Salary.MachineLearning;
using Salary.Models;
using Salary.Services;

namespace Salary.Controllers
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

            var prediction = SalaryPredictionService.GetPrediction(Program.TrainedModel, employee);
            Print.PredictedSalary(prediction);
        }
    }
}