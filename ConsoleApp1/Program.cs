using System.Collections.Generic;
using PredictSalary.Domain;

namespace PredictSalary
{
    class Program
    {
        public static readonly IList<Employee> Data = new List<Employee>();

        static void Main(string[] args)
        {
            MainMenu.Show();
        }
    }
}
