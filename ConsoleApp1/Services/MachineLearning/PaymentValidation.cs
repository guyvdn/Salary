using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Salary.Domain;

namespace Salary.Services.MachineLearning
{
    public class PaymentValidation
    {
        public class SpikePrediction
        {
            //vector to hold alert, score, p-value
            [VectorType(3)]
            public double[] Prediction { get; set; }
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);

        public static void Validate(IList<Payment> data)
        {
            var dataView = MlContext.Data.LoadFromEnumerable(data);
            var estimator = MlContext.Transforms.DetectIidSpike(nameof(SpikePrediction.Prediction), nameof(Payment.Amount), 99, data.Count / 4);
            var transformer = estimator.Fit(dataView);
            var transformedData = transformer.Transform(dataView);
            var predictions = MlContext.Data.CreateEnumerable<SpikePrediction>(transformedData, false).ToList();

            Print.SpikePredictions(predictions);
        }
    }
}