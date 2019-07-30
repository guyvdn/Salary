using System.Linq;

namespace Salary.Services
{
    public static class Validate
    {
        public static bool DataIsLoaded()
        {
            if (Program.Data.Any())
                return true;

            Print.Error("No data has been loaded yet");
            return false;
        }

        public static bool ModelIsTrained()
        {
            if (Program.TrainedModel != null)
                return true;

            Print.Error("The model has not been trained yet");
            return false;
        }
    }
}