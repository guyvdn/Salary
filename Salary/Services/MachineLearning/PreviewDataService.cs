using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Salary.Services.MachineLearning
{
    public static class PreviewDataService
    {
        public static IEnumerable<string> GetPreviewData<T>(EstimatorChain<T> pipeline, IDataView dataView, int maxRows = 10) where T : class, ITransformer
        {
            var transformer = pipeline.Fit(dataView);
            var transformedData = transformer.Transform(dataView);
            var previewData = transformedData.Preview(maxRows).RowView;

            var sparseVectorData = new Dictionary<string, List<float[]>>();

            foreach (var column in transformedData.Schema)
            {
                if (column.Type.RawType == typeof(VBuffer<float>))
                {
                    sparseVectorData.Add(column.Name, transformedData.GetColumn<float[]>(column).Take(maxRows).ToList());
                }
            }
            
            string SparseVector(IEnumerable<float> vector)
            {
                return "[" + string.Join(",", vector) + "]";
            }

            string GetValue(string key, object value, int i)
            {
                return value is VBuffer<float> ? SparseVector(sparseVectorData[key][i]) : value.ToString();
            }

            for (var i = 0; i < previewData.Length; i++)
            {
                var data = new List<string>();
                foreach (var (key, value) in previewData[i].Values)
                {
                    data.Add($"{key}: {GetValue(key, value, i)}");
                }
                yield return (string.Join(" | ", data));
            }
        }
    }
}