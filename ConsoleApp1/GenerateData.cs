using System;
using System.Collections.Generic;

namespace PredictSalary
{
    public class ExperienceLevel
    {
        public string DisplayName { get; }
        public double SalaryMultiplier { get; }
        public static List<ExperienceLevel> Values { get; } = new List<ExperienceLevel>();

        public static ExperienceLevel Junior = new ExperienceLevel("Junior", 0.95);
        public static ExperienceLevel Medior = new ExperienceLevel("Medior", 1);
        public static ExperienceLevel Senior = new ExperienceLevel("Senior", 1.05);

        private ExperienceLevel(string displayName, double salaryMultiplier)
        {
            DisplayName = displayName;
            SalaryMultiplier = salaryMultiplier;
            Values.Add(this);
        }

        public override string ToString() => DisplayName;
    }

    public static class GenerateData
    {
        private static readonly Random Random = new Random();

        public static void Execute()
        {
            var numberOfEmployeesToGenerate = ConsoleHelper.EnterNumber("How many employees do you want to generate?");
            
            Console.WriteLine("Generating Employees");

            for (var i = 0; i < numberOfEmployeesToGenerate; i++)
            {
                var age = Random.Next(20, 50);
                var experienceLevel = ExperienceLevel.Values[Random.Next(0, 3)];
                var salary = CalculateSalary(age, experienceLevel);
                var employee = new Employee(age, experienceLevel, salary);
                Console.WriteLine(employee);
            }
        }

        private static double CalculateSalary(int age, ExperienceLevel experienceLevel)
        {
            var randomDeviation = 1 + (Random.NextDouble() * 4 - 2) / 100;
            return Math.Round(age * 100 * experienceLevel.SalaryMultiplier * randomDeviation);
        }
    }
}