using System.Collections.Generic;

namespace Salary.Models
{
    public class ExperienceLevel
    {
        public string DisplayName { get; }
        public double SalaryMultiplier { get; }
        public static List<ExperienceLevel> Values { get; } = new List<ExperienceLevel>();

        public static ExperienceLevel Junior = new ExperienceLevel("Junior", 0.90);
        public static ExperienceLevel Medior = new ExperienceLevel("Medior", 1);
        public static ExperienceLevel Senior = new ExperienceLevel("Senior", 1.10);

        private ExperienceLevel(string displayName, double salaryMultiplier)
        {
            DisplayName = displayName;
            SalaryMultiplier = salaryMultiplier;
            Values.Add(this);
        }

        public override string ToString() => DisplayName;
    }
}