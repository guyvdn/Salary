using Microsoft.ML;
using Microsoft.ML.Data;
using Salary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salary.MachineLearning
{
    public static class SpikeDetectionService
    {
        private class SpikePredictionDto : Payment
        {
            //vector to hold alert, score, p-value
            [VectorType(3)]
            public double[] Prediction { get; set; }
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);

        public static IEnumerable<PaymentSpikePrediction> DetectSpikes(IList<Payment> data, int confidence = 95)
        {
            const string inputColumnName = nameof(Payment.Amount);
            const string outputColumnName = nameof(SpikePredictionDto.Prediction);

            var pvalueHistoryLength = data.Count / 4;
            var estimator = MlContext.Transforms.DetectIidSpike(outputColumnName, inputColumnName, confidence, pvalueHistoryLength);

            var dataView = MlContext.Data.LoadFromEnumerable(data);
            var transformer = estimator.Fit(dataView);
            var transformedData = transformer.Transform(dataView);

            var predictions = MlContext.Data.CreateEnumerable<SpikePredictionDto>(transformedData, false).ToList();

            foreach (var prediction in predictions)
            {
                yield return new PaymentSpikePrediction
                {
                    Date = prediction.Date,
                    Amount = prediction.Amount,
                    IsSpike = Math.Abs(prediction.Prediction[0] - 1) < 0.001,
                    PValue = prediction.Prediction[2]
                };
            }
        }
    }
}