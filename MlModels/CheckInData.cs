using Microsoft.ML.Data;

namespace OdontoPrevAPI.MlModels
{
    public class CheckInData
    {
        [LoadColumn(0)]
        public long PerguntaId { get; set; }

        [LoadColumn(1)]
        public long RespostaId { get; set; }

        [LoadColumn(2)]
        public string TextoPergunta { get; set; } = string.Empty;

        [LoadColumn(3)]
        public string TextoResposta { get; set; } = string.Empty;

        [LoadColumn(4)]
        [ColumnName("Label")]
        public bool HasIssue { get; set; }

        [LoadColumn(5)]
        public long PacienteId { get; set; }
    }
    
    /// <summary>
    /// Classe para representar um conjunto de check-ins para análise contextual
    /// </summary>
    public class ContextualCheckInData
    {
        // Dados básicos
        public int PacienteId { get; set; }

        // Texto agregado das perguntas e respostas dos últimos 5 dias
        public string ConsolidatedQuestions { get; set; } = string.Empty;
        public string ConsolidatedAnswers { get; set; } = string.Empty;

        // Estatísticas importantes
        public int CheckInCount { get; set; }
        public DateTime MostRecentDate { get; set; }
        public DateTime OldestDate { get; set; }

        // Tópicos abordados (para melhor contextualização)
        public bool ContainsPainTopic { get; set; }
        public bool ContainsBleedingTopic { get; set; }
        public bool ContainsSensitivityTopic { get; set; }
        public bool ContainsBruxismTopic { get; set; }

        // Rótulo para treinamento
        [ColumnName("Label")]
        public bool HasIssue { get; set; }
    }

    /// <summary>
    /// Resultado da predição contextual
    /// </summary>
    public class ContextualIssuePrediction
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedIssue { get; set; }

        public float Score { get; set; }

        public float Probability => 1 / (1 + MathF.Exp(-Score));

        // Índice de recomendação sugerido pelo modelo
        public int SuggestedRecommendationIndex { get; set; }
    }

    public class DentalAnalysisResult
    {
        public DateTime CheckInDate { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public bool PotentialIssue { get; set; }
        public float Confidence { get; set; }
        public string Recommendation { get; set; } = string.Empty;
    }

    public class PredictionResult
    {
        public DateTime CheckInDate { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public bool PotentialIssue { get; set; }
        public float Confidence { get; set; }
        public string Recommendation { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public int TotalCheckInsAnalyzed { get; set; }
    }

    public class PatientDentalSummary
    {
        public int PacienteId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int TotalCheckIns { get; set; }
        public int ProblemasIdentificados { get; set; }
        public float ConfiancaMedia { get; set; }
        public string RecomendacaoPrincipal { get; set; } = string.Empty;
        public DateTime UltimoCheckIn { get; set; }
        public List<DentalAnalysisResult> DetalhesAnalise { get; set; } = new List<DentalAnalysisResult>();
    }
}
