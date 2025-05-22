using Microsoft.ML.Data;

namespace OdontoPrevAPI.MlModels
{
    public class IssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedIssue { get; set; }

        public float Score { get; set; }

        public float Probability => 1 / (1 + MathF.Exp(-Score));
    }
}
