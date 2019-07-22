using System;

namespace PredictSalary.Domain
{
    public class Employee
    {
        private static readonly Random Random = new Random();

        public Employee(int age, ExperienceLevel experienceLevel, int salary) 
            : this(age, experienceLevel)
        {
            Salary = salary;
        }

        public Employee(int age, ExperienceLevel experienceLevel)
        {
            Age = age;
            //ExperienceLevel = experienceLevel;
            ExperienceLevel = ExperienceLevel.Medior;
            BaseSalary = (int)(Age * 100 * ExperienceLevel.SalaryMultiplier);
            //MinimumSalary = (int)(BaseSalary * 0.98);
            //MaximumSalary = (int)(BaseSalary * 1.02);
            MinimumSalary = BaseSalary;
            MaximumSalary = BaseSalary;
            Salary = Random.Next(MinimumSalary, MaximumSalary);
        }

        public int Age { get; }
        public ExperienceLevel ExperienceLevel { get; }
        public int BaseSalary { get; }
        public int MinimumSalary { get; }
        public int MaximumSalary { get; }
        public int Salary { get; }

        public override string ToString() => $"Age: {Age} - ExperienceLevel: {ExperienceLevel} - Salary: {Salary:N0}";
    }
}