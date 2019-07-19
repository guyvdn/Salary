using System;

namespace PredictSalary
{
    public class ConsoleAction
    {
        public Action Action { get; }
        private readonly string displayName;

        public ConsoleAction(string displayName, Action action)
        {
            Action = action;
            this.displayName = displayName;
        }

        public override string ToString() => displayName;
    }
}