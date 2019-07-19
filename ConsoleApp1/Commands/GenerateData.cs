using System;
using PredictSalary.Domain;
using PredictSalary.Infrastructure;

namespace PredictSalary.Commands
{
    public static class GenerateData
    {
        private static readonly Random Random = new Random();

        public static void Execute()
        {
            var numberOfEmployeesToGenerate = ConsoleHelper.GetNumber("How many employees do you want to generate?");

            Console.WriteLine();
            Console.WriteLine("Generating Employees");

            for (var i = 0; i < numberOfEmployeesToGenerate; i++)
            {
                var age = Random.Next(20, 50);
                var experienceLevel = ExperienceLevel.Values[Random.Next(0, 3)];
                var employee = new Employee(age, experienceLevel);
                Console.WriteLine(employee);
            }
        }
    }
}