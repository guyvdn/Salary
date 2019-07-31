using System;
using System.Collections.Generic;
using Salary.Domain;
using Salary.Infrastructure;
using Salary.Services;
using Salary.Services.MachineLearning;

namespace Salary.Commands
{
    public static class SpikeDetection
    {
        public static void Execute()
        {
            Print.Header("Spike Detection");

            var age = ConsoleHelper.GetNumber("Enter Age of Employee:");
            var experienceLevel = ConsoleHelper.PickOption(ExperienceLevel.Values);

            var startData = new List<Payment>();
            for (var i = -36; i < 0; i++)
            {
                startData.Add(new Payment
                {
                    Date = DateTime.Today.AddMonths(i),
                    Amount = new Employee(age, experienceLevel).Salary
                });
            }

            var employee = new Employee(age, experienceLevel);
            
            var continueTest = true;
            while (continueTest)
            {
                Print.Employee(employee);

                var amount = ConsoleHelper.GetNumber("Enter Amount for Spike detection:");
                var data = new List<Payment>(startData) { new Payment { Date = DateTime.Today, Amount = amount } };
                
                PaymentValidation.Validate(data);
                
                Console.WriteLine();
                continueTest = ConsoleHelper.GetYesNo("Try again?");
                Console.WriteLine();
            }
        }
    }
}