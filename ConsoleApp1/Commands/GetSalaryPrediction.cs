using System;
using PredictSalary.Domain;
using PredictSalary.Infrastructure;

namespace PredictSalary.Commands
{
    public class GetSalaryPrediction
    {
        public static void Execute()
        {
            Console.WriteLine("Get Salary Prediction");
            Console.WriteLine();

            var age = ConsoleHelper.GetNumber("Enter Age of Employee:");
            var experienceLevel = ConsoleHelper.PickOption(ExperienceLevel.Values);

            var employee = new Employee(age, experienceLevel, -1);

            Console.WriteLine(employee);
            Console.WriteLine($"BaseSalary: {employee.BaseSalary} - MinimumSalary: {employee.MinimumSalary} - MaximumSalary: {employee.MaximumSalary}");
        }
    }
}