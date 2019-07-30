﻿using System;

namespace Salary.Domain
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
            ExperienceLevel = experienceLevel;
            BaseSalary = (int)(Age * 100 * ExperienceLevel.SalaryMultiplier);
            MinimumSalary = (int)(BaseSalary * 0.98);
            MaximumSalary = (int)(BaseSalary * 1.02);
            Salary = Random.Next(MinimumSalary, MaximumSalary);
        }

        public int Age { get; }
        public ExperienceLevel ExperienceLevel { get; }
        public int BaseSalary { get; }
        public int MinimumSalary { get; }
        public int MaximumSalary { get; }
        public int Salary { get; }
    }
}