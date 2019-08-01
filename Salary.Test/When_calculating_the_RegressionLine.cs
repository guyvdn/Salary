using NUnit.Framework;
using Salary.MachineLearning;
using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Salary.Test
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class When_calculating_the_RegressionLine
    {
        [Test]
        public void It_should_return_the_correct_result()
        {
            var points = new List<(float x, float y)> { (1, 2), (2, 1), (4, 3) };

            StatisticsService.CalculateRegressionLine(points, 0, 7, out var y1, out var y2);


            y1.ShouldBe(1, .0001);
            y2.ShouldBe(4, .0001);
        }

        [Test]
        public void It_should_return_the_correct_result_of_a_more_complex_problem()
        {
            var points = new List<(float x, float y)> { (-2, -3), (-1, -1), (1, 2), (4, 3) };

            StatisticsService.CalculateRegressionLine(points, -10, 32, out var y1, out var y2);

            // y = 41x/42 - 5/21
            y1.ShouldBe(-10, .0001);
            y2.ShouldBe(31, .0001);
        }
    }
}