using System;
using System.Collections.Generic;
using PredictSalary.Commands;
using PredictSalary.Infrastructure;

namespace PredictSalary
{
    public static class MainMenu
    {
        public static void Show()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(" What do you want to do?");
            Console.WriteLine("--------------------------------------------------");

            var options = new List<ConsoleAction>
            {
                new ConsoleAction("Generate Data", GenerateData.Execute),
                new ConsoleAction("Train Model", MachineLearning.Learn),
                new ConsoleAction("Get Salary Prediction", GetSalaryPrediction.Execute)
            };

            ConsoleHelper.PickOption(options).Execute();
        } 
    }
}