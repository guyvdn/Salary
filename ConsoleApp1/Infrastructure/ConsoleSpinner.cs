using System;
using System.Threading;
using System.Threading.Tasks;

namespace Salary.Infrastructure
{
    public static class ConsoleSpinner
    {
        public static void Execute(string message, Action action)
        {
            ExecuteInternal(message, Task.Run(action));
        }    
        
        public static T Execute<T>(string message, Func<T> func)
        {
            var task = Task.Run(func);
            ExecuteInternal(message, task);
            return task.Result;
        }

        private static void ExecuteInternal(string message, Task task)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{message} ");
            Console.CursorVisible = false;

            var spinCounter = 0;

            while (!task.IsCompleted)
            {
                switch (spinCounter++ % 4)
                {
                    case 0:
                        Console.Write(@"/");
                        break;
                    case 1:
                        Console.Write(@"-");
                        break;
                    case 2:
                        Console.Write(@"\");
                        break;
                    case 3:
                        Console.Write(@"|");
                        break;
                }

                Console.Write("\b");
                Thread.Sleep(100);
            }

            Console.CursorVisible = true;

            if (!task.IsFaulted)
            {
                Console.WriteLine("done");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("failed");
                Console.ResetColor();

                if (task.Exception != null)
                    throw task.Exception;
            }
        }
    }
}