using System.Collections.Generic;
using Microsoft.ML;
using Salary.Domain;

namespace Salary
{
    class Program
    {
        internal static IList<Employee> Data { get; } = new List<Employee>();
        internal static ITransformer TrainedModel { get; set; }

        private static void Main(string[] args)
        {
            MainMenu.Show();
        }
    }
}
