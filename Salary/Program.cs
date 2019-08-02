using System.Collections.Generic;
using Microsoft.ML;
using Salary.Models;

namespace Salary
{
    class Program
    {
        internal static IList<Employee> TrainingData { get; } = new List<Employee>();
        internal static IList<Employee> TestData { get; } = new List<Employee>();
        
        internal static ITransformer TrainedModel { get; set; }

        private static void Main(string[] args)
        {
            MainMenu.Show();
        }
    }
}
