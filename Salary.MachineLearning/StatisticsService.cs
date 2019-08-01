using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Salary.MachineLearning
{
    public static class StatisticsService
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void CalculateRegressionLine(IList<(float x, float y)> points, int starpointX, int endpointX, out float startpointY, out float endpointY)
        {
            // Regression Line calculation explanation:
            // https://www.khanacademy.org/math/statistics-probability/describing-relationships-quantitative-data/more-on-regression/v/regression-line-example

            var count = points.Count;
            var sumX = points.Sum(point => point.x);
            var sumY = points.Sum(point => point.y);
            var sumXX = points.Sum(point => point.x * point.x);
            var sumXY = points.Sum(point => point.x * point.y);

            var meanX = sumX / count;
            var meanY = sumY / count;
            var meanXY = sumXY / count;
            var meanXX = sumXX / count;

            var m = (meanX * meanY - meanXY) / (meanX * meanX - meanXX);
            var b = meanY - m * meanX;

            startpointY = m * starpointX + b;
            endpointY = m * endpointX + b;
        }
    }
}