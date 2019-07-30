using System;
using System.Collections.Generic;
using System.Linq;

namespace Salary.Infrastructure
{
    public static class ConsoleHelper
    {
        public static T PickOption<T>(IEnumerable<T> options)
        {
            var optionsArray = options.ToArray();

            for (var i = 0; i < optionsArray.Length; i++)
            {
                var option = optionsArray[i];
                WriteLine($"{i + 1}. {option}", ConsoleColor.Yellow);
            }

            var selectedOption = GetNumber("Make your choice:", optionsArray.Length);

            return optionsArray[selectedOption - 1];
        }

        public static int GetNumber(string question, int maxValue = int.MaxValue)
        {
            var selectedOption = -1;

            while (selectedOption < 1 || selectedOption > maxValue)
            {
                Console.WriteLine();
                Write(question + " ", ConsoleColor.Cyan);
                int.TryParse(Console.ReadLine(), out selectedOption);
            }

            return selectedOption;
        }

        public static bool GetYesNo(string question)
        {
            Write($"{question} (y/n) ", ConsoleColor.Cyan);

            while (true)
            {
                var input = Console.ReadKey().KeyChar;

                if (input == 'y' || input == 'Y')
                    return true;

                if (input == 'n' || input == 'N')
                    return false;

                Console.Write("\b \b");
            }
        }

        public static void Write(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }

        public static void WriteLine(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static void WithColor(ConsoleColor color, Action action)
        {
            Console.ForegroundColor = color;
            action.Invoke();
            Console.ResetColor();
        }

        public static void WriteLines(string[] lines)
        {
            Array.ForEach(lines, Console.WriteLine);
        }

        public static void WriteLines(string[] lines, ConsoleColor color)
        {
            Array.ForEach(lines, line => WriteLine(line, color));
        }
    }
}