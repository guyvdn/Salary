using System;

namespace Salary.Infrastructure
{
    public class ConsoleAction
    {
        private readonly Action action;
        private readonly string displayName;

        public ConsoleAction(string displayName, Action action)
        {
            this.action = action;
            this.displayName = displayName;
        }

        public void Execute()
        {
            Console.Clear();
            action.Invoke();

            Console.WriteLine("");
            ConsoleHelper.Write("Press any key to continue...", ConsoleColor.Cyan);
            Console.ReadKey();

            MainMenu.Show();
        } 

        public override string ToString() => displayName;
    }
}