using System;

namespace PredictSalary.Infrastructure
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
            action.Invoke();
            Console.WriteLine();
            MainMenu.Show();
        } 

        public override string ToString() => displayName;
    }
}