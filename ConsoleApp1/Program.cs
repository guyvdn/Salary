using System;
using System.Collections.Generic;

namespace PredictSalary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();

            var options = new List<ConsoleAction>
            {
                new ConsoleAction("Generate Data", GenerateData.Execute)
            };

            var selectedOption = ConsoleHelper.PickOption(options);
            selectedOption.Action();
        }
    }
}
