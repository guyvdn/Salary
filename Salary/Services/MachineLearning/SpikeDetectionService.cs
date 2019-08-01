using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using Salary.Domain;

namespace Salary.Services.MachineLearning
{
    public static class SpikeDetectionService
    {
        public class SpikePrediction
        {
            //vector to hold alert, score, p-value
            [VectorType(3)]
            public double[] Prediction { get; set; }

            public bool IsSpike => Math.Abs(Prediction[0] - 1) < 0.001;
            public double Amount => Prediction[1];
            public double PValue => Prediction[2];
        }

        private static readonly MLContext MlContext = new MLContext(seed: 0);

        public static IEnumerable<SpikePrediction> DetectSpikes(IList<Payment> data, int confidence = 95)
        {
            const string inputColumnName = nameof(Payment.Amount);
            const string outputColumnName = nameof(SpikePrediction.Prediction);

            var pvalueHistoryLength = data.Count / 4;
            var estimator = MlContext.Transforms.DetectIidSpike(outputColumnName, inputColumnName, confidence, pvalueHistoryLength);

            var dataView = MlContext.Data.LoadFromEnumerable(data);
            var transformer = estimator.Fit(dataView);
            var transformedData = transformer.Transform(dataView);

            return MlContext.Data.CreateEnumerable<SpikePrediction>(transformedData, false);
        }
    }
}