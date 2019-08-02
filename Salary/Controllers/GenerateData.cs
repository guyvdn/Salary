using Salary.Infrastructure;
using Salary.Models;
using Salary.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salary.Controllers
{
    public static class GenerateData
    {
        private static readonly Random Random = new Random();
        private const int NumberOfEmployeesToGenerate = 100_000;

        public static void Execute()
        {
            Print.Header("Generate Data");

            Program.TrainingData.Clear();
            Program.TestData.Clear();

            Console.WriteLine();

            ConsoleSpinner.Execute($"Generating {NumberOfEmployeesToGenerate * 2:N0} Employees", () =>
            {
                AddEmployees(Program.TrainingData, NumberOfEmployeesToGenerate);
                AddEmployees(Program.TestData, NumberOfEmployeesToGenerate);
            });

            Print.PreviewGeneratedData(Program.TrainingData.Take(10));
        }

        private static void AddEmployees(ICollection<Employee> dataset, int numberOfEmployeesToGenerate)
        {
            for (var i = 0; i < numberOfEmployeesToGenerate; i++)
            {
                var age = Random.Next(20, 60);
                var experienceLevel = ExperienceLevel.Values[Random.Next(0, 3)];
                dataset.Add(new Employee(age, experienceLevel));
            }
        }
    }
}