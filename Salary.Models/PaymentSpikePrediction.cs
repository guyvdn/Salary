namespace Salary.Models
{
    public class PaymentSpikePrediction : Payment
    {
        public bool IsSpike { get; set; }
        public double PValue { get; set; }
    }
}