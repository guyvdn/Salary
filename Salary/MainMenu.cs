using System;
using System.Collections.Generic;
using Salary.Commands;
using Salary.Infrastructure;
using Salary.Services;

namespace Salary
{
    public static class MainMenu
    {
        public static void Show()
        {
            Console.Clear();
            Print.Header("What do you want to do?");

            var options = new List<ConsoleAction>
            {
                new ConsoleAction("Generate Data", GenerateData.Execute),
                new ConsoleAction("Train Model", TrainModel.Execute),
                new ConsoleAction("Get Salary Prediction", GetSalaryPrediction.Execute),
                new ConsoleAction("Detect Spikes", DetectSpikes.Execute),
                new ConsoleAction("Exit", ()=> Environment.Exit(0))
            };

            ConsoleHelper.PickOption(options).Execute();
        } 
    }
}