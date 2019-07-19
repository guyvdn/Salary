using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictSalary
{
    public static class ConsoleHelper
    {
        public static T PickOption<T>(IEnumerable<T> options)
        {
            var optionsArray = options.ToArray();
            

            for (var i = 0; i < optionsArray.Length; i++)
            {
                var option = optionsArray[i];
                Console.WriteLine($"{i + 1}. {option}");
            }

            var selectedOption = EnterNumber("Make your choice:", optionsArray.Length);

            return optionsArray[selectedOption - 1];
        }

        public static int EnterNumber(string question, int maxValue = int.MaxValue)
        {
            var selectedOption = -1;

            while (selectedOption < 1 || selectedOption > maxValue)
            {
                Console.WriteLine();
                Console.Write(question + " ");
                int.TryParse(Console.ReadLine(), out selectedOption);
            }

            return selectedOption;
        }
    }
}