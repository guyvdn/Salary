namespace PredictSalary
{
    public class Employee
    {
        public Employee(int age, ExperienceLevel experienceLevel, double salary)
        {
            Age = age;
            ExperienceLevel = experienceLevel;
            Salary = salary;
        }

        public int Age { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public double Salary { get; set; }

        public override string ToString() => $"Age: {Age} - ExperienceLevel: {ExperienceLevel} - Salary: {Salary}";
    }
}