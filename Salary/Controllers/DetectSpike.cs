﻿using System;
using System.Collections.Generic;
using System.Linq;
using Salary.Infrastructure;
using Salary.MachineLearning;
using Salary.Models;
using Salary.Services;

namespace Salary.Controllers
{
    public static class DetectSpike
    {
        public static void Execute()
        {
            Print.Header("Detect Spike");

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

                var predictions = SpikeDetectionService.DetectSpikes(data).ToList();
                Print.PaymentPredictions(predictions);

                const string fileName = "payments.png";
                Plot.PaymentChart(predictions, fileName);
                Plot.ShowImage(fileName);

                Console.WriteLine();
                continueTest = ConsoleHelper.GetYesNo("Try again?");
                Console.WriteLine();
            }
        }
    }
}