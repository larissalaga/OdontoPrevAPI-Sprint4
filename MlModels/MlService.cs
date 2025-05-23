using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Trainers.FastTree;

namespace OdontoPrevAPI.MlModels
{
    public class MlService
    {
        private readonly MLContext _mlContext;
        private readonly DataContext _dataContext;
        private readonly GenerativeAIService _generativeAI;        
        private ITransformer? _contextualModel;  // NOVO: Modelo específico para análise contextual
        private readonly Dictionary<int, string> _cachedRecommendations;

        // Lista predefinida de recomendações odontológicas comuns
        private readonly List<string> _commonRecommendations = new List<string>
        {
            "Recomendamos agendar uma limpeza profissional a cada 6 meses para remover o tártaro que não pode ser eliminado com escovação regular e prevenir problemas futuros.",
            "Escove os dentes pelo menos duas vezes ao dia durante 2 minutos, utilizando movimentos circulares suaves e prestando atenção especial à linha da gengiva.",
            "Utilize fio dental diariamente para remover a placa bacteriana e resíduos de alimentos entre os dentes, áreas que a escova não alcança efetivamente.",
            "Para reduzir a sensibilidade dentária, utilize pasta de dente específica para dentes sensíveis e evite alimentos e bebidas muito quentes, frias ou ácidas.",
            "A dor que você relatou pode indicar uma infecção ou cárie profunda. Recomendamos consulta odontológica urgente para avaliação e tratamento adequado.",
            "Seus sintomas sugerem gengivite inicial. Intensifique a higiene bucal, use enxaguante bucal antisséptico e agende uma consulta para avaliação profissional.",
            "O desgaste dentário observado sugere bruxismo noturno. Recomendamos a confecção de uma placa de mordida para uso durante o sono.",
            "Para aliviar a boca seca, aumente a ingestão de água, evite cafeína e álcool, e considere o uso de substitutos de saliva recomendados pelo seu dentista.",
            "Reduza o consumo de alimentos e bebidas açucaradas ou ácidas, como refrigerantes e sucos cítricos, para prevenir a erosão do esmalte e o desenvolvimento de cáries.",
            "Baseado em seus relatos recentes, recomendamos um exame bucal completo, incluindo radiografias, para identificar possíveis problemas em estágio inicial."
        };
                
        // Dicionário para mapear histórico de pacientes para recomendações
        // Chave: ID do paciente, Valor: índice da recomendação
        private readonly Dictionary<int, int> _patientRecommendationMap;

        // Dicionário para mapear combinações de pergunta-resposta para índices de recomendação
        private readonly Dictionary<(int perguntaId, int respostaId), int> _recommendationMap;

        public MlService(DataContext dataContext, GenerativeAIService generativeAI)
        {
            _mlContext = new MLContext(seed: 0);
            _dataContext = dataContext;
            _generativeAI = generativeAI;
            _cachedRecommendations = new Dictionary<int, string>();
            _recommendationMap = new Dictionary<(int perguntaId, int respostaId), int>();
            _patientRecommendationMap = new Dictionary<int, int>();
        }

        /// <summary>
        /// Treina os modelos ML usando dados de check-in dos últimos 5 dias por paciente
        /// </summary>
        public async Task TrainModelFromDatabase()
        {
            Console.WriteLine("Iniciando treinamento dos modelos com análise contextual de 5 dias...");

            // Obter todos os check-ins com suas entidades relacionadas
            var allCheckIns = await _dataContext.CheckIn
                .Include(c => c.Perguntas)
                .Include(c => c.Respostas)
                .ToListAsync();

            // Agrupar check-ins por paciente
            var patientCheckIns = allCheckIns
                .GroupBy(c => c.IdPaciente)
                .ToDictionary(g => g.Key, g => g.ToList());

            var individualTrainingData = new List<CheckInData>();
            var contextualTrainingData = new List<ContextualCheckInData>();

            _recommendationMap.Clear();
            _patientRecommendationMap.Clear();

            // Processar os dados de cada paciente com análise de 5 dias
            foreach (var patientId in patientCheckIns.Keys)
            {
                var patientData = patientCheckIns[patientId];

                // Encontrar a data mais recente de check-in para este paciente
                var mostRecentDate = patientData.Max(c => c.DtCheckIn);

                // Obter check-ins dos últimos 5 dias
                var recentCheckIns = patientData
                    .Where(c => (mostRecentDate - c.DtCheckIn).TotalDays <= 5)
                    .OrderByDescending(c => c.DtCheckIn)
                    .ToList();

                Console.WriteLine($"Paciente {patientId}: Encontrados {recentCheckIns.Count} check-ins na janela de 5 dias");

                // Analisar o histórico completo do paciente para determinar recomendações
                AnalyzePatientHistoryAndCreateRecommendation(patientId, recentCheckIns);

                // NOVO: Criar dados de treinamento contextual para estes 5 dias
                if (recentCheckIns.Count > 0)
                {
                    // Determinar se o paciente tem algum problema potencial
                    bool hasPatientIssue = false;
                    foreach (var checkIn in recentCheckIns)
                    {
                        if (DetermineIfPotentialIssue(checkIn, recentCheckIns))
                        {
                            hasPatientIssue = true;
                            break;
                        }
                    }

                    // Criar entrada de dados contextuais para este paciente
                    var contextualData = new ContextualCheckInData
                    {
                        PacienteId = patientId,
                        CheckInCount = recentCheckIns.Count,
                        MostRecentDate = recentCheckIns.First().DtCheckIn,
                        OldestDate = recentCheckIns.Last().DtCheckIn,

                        // Consolidar todas as perguntas e respostas
                        ConsolidatedQuestions = string.Join(" | ", recentCheckIns
                            .Where(c => c.Perguntas != null)
                            .Select(c => c.Perguntas.DsPergunta)),

                        ConsolidatedAnswers = string.Join(" | ", recentCheckIns
                            .Where(c => c.Respostas != null)
                            .Select(c => c.Respostas.DsResposta)),

                        // Detectar tópicos relevantes
                        ContainsPainTopic = recentCheckIns.Any(c =>
                            c.Perguntas?.DsPergunta.ToLower().Contains("dor") == true),

                        ContainsBleedingTopic = recentCheckIns.Any(c =>
                            c.Perguntas?.DsPergunta.ToLower().Contains("sangr") == true ||
                            c.Perguntas?.DsPergunta.ToLower().Contains("gengiva") == true),

                        ContainsSensitivityTopic = recentCheckIns.Any(c =>
                            c.Perguntas?.DsPergunta.ToLower().Contains("sensibilidade") == true),

                        ContainsBruxismTopic = recentCheckIns.Any(c =>
                            c.Perguntas?.DsPergunta.ToLower().Contains("range") == true ||
                            c.Perguntas?.DsPergunta.ToLower().Contains("mandíbula") == true),

                        // Rótulo para treinamento
                        HasIssue = hasPatientIssue
                    };

                    contextualTrainingData.Add(contextualData);
                }

                // Também criar dados de treinamento para cada check-in individual (para compatibilidade)
                foreach (var checkIn in recentCheckIns)
                {
                    if (checkIn.Perguntas != null && checkIn.Respostas != null)
                    {
                        individualTrainingData.Add(new CheckInData
                        {
                            PerguntaId = checkIn.IdPergunta,
                            RespostaId = checkIn.IdResposta,
                            PacienteId = checkIn.IdPaciente,
                            TextoPergunta = checkIn.Perguntas.DsPergunta,
                            TextoResposta = checkIn.Respostas.DsResposta,
                            HasIssue = DetermineIfPotentialIssue(checkIn, recentCheckIns)
                        });

                        // Mapear combinações pergunta-resposta para recomendações
                        MapQuestionAnswerToRecommendation(checkIn.IdPergunta, checkIn.IdResposta);
                    }
                }
            }

            Console.WriteLine($"Treinando modelo contextual com {contextualTrainingData.Count} conjuntos de pacientes");
            TrainContextualModel(contextualTrainingData);

            Console.WriteLine("Treinamento de ambos os modelos concluído com sucesso");
        }

        /// <summary>
        /// Analisa o histórico completo de check-ins de um paciente e cria uma recomendação personalizada
        /// </summary>
        private void AnalyzePatientHistoryAndCreateRecommendation(int patientId, List<CheckIn> recentCheckIns)
        {
            if (recentCheckIns.Count == 0)
                return;

            // Estatísticas para determinar a recomendação mais apropriada
            Dictionary<int, int> perguntaRelevance = new Dictionary<int, int>();
            Dictionary<int, List<int>> respostaDistribution = new Dictionary<int, List<int>>();

            // Contagem de problemas potenciais por categoria
            int sensitivityIssues = 0;
            int bleedingIssues = 0;
            int painIssues = 0;
            int hygieneIssues = 0;
            int dryMouthIssues = 0;
            int sugarConsumption = 0;
            int bruxismIssues = 0;
            int examNeeded = 0;

            // Analisar cada check-in
            foreach (var checkIn in recentCheckIns)
            {
                // Analisar conteúdo da pergunta e resposta para detectar problemas
                if (checkIn.Perguntas != null && checkIn.Respostas != null)
                {
                    string pergunta = checkIn.Perguntas.DsPergunta.ToLower();
                    string resposta = checkIn.Respostas.DsResposta.ToLower();

                    // Sensibilidade - Ampliado para detectar menções de sensibilidade de várias formas
                    if (pergunta.Contains("sensibilidade") &&
                        (resposta.Contains("sim") || resposta.Contains("muito") || resposta.Contains("forte") ||
                         resposta.Contains("sinto") || !resposta.Contains("não") && !resposta.Contains("nunca")))
                    {
                        sensitivityIssues++;
                    }

                    // Sangramento - Melhorado para detectar qualquer menção a sangramento gengival
                    if ((pergunta.Contains("gengiva") || pergunta.Contains("sangr")) &&
                        (resposta.Contains("sim") || resposta.Contains("às vezes") ||
                         resposta.Contains("sangra") || resposta.Contains("sangue")))
                    {
                        bleedingIssues++;
                    }

                    // Dor - Expandido para capturar mais indicações de dor
                    if ((pergunta.Contains("dor") || pergunta.Contains("desconforto")) &&
                        (resposta.Contains("sim") || resposta.Contains("forte") ||
                         resposta.Contains("constant") || resposta.Contains("intens") ||
                         resposta.Contains("pulsa") || resposta.Contains("mast")))
                    {
                        painIssues++;
                    }

                    // Higiene - Ampliado para melhor detecção de problemas de higiene
                    if ((pergunta.Contains("escova") || pergunta.Contains("fio dental") ||
                         pergunta.Contains("higiene")) &&
                        (resposta.Contains("raramente") || resposta.Contains("nunca") ||
                         resposta.Contains("uma vez") || resposta.Contains("não uso") ||
                         resposta.Contains("não tenho") || resposta.Contains("desconfort")))
                    {
                        hygieneIssues++;
                    }

                    // Boca seca - Melhorado para capturar várias indicações
                    if ((pergunta.Contains("boca seca") || pergunta.Contains("saliva")) &&
                        (resposta.Contains("sim") || resposta.Contains("frequente") ||
                         resposta.Contains("constante") || resposta.Contains("medicamento")))
                    {
                        dryMouthIssues++;
                    }

                    // Açúcar - Expandido para incluir refrigerantes e outros alimentos ácidos
                    if ((pergunta.Contains("açúcar") || pergunta.Contains("doce") ||
                         pergunta.Contains("refrigerante") || pergunta.Contains("bebida") ||
                         pergunta.Contains("alimento")) &&
                        (resposta.Contains("frequentemente") || resposta.Contains("muito") ||
                         resposta.Contains("diariamente") || resposta.Contains("várias vezes")))
                    {
                        sugarConsumption++;
                    }

                    // Bruxismo - Ampliado para incluir sintomas relacionados
                    if ((pergunta.Contains("range") || pergunta.Contains("mandíbula") ||
                         pergunta.Contains("desgaste") || pergunta.Contains("dentes")) &&
                        (resposta.Contains("sim") || resposta.Contains("frequentemente") ||
                         resposta.Contains("acord") || resposta.Contains("dor") ||
                         resposta.Contains("estala") || resposta.Contains("tensão")))
                    {
                        bruxismIssues++;
                    }

                    // Exame - Melhorado para capturar tempo sem consulta
                    if ((pergunta.Contains("tratamento") || pergunta.Contains("consulta") ||
                         pergunta.Contains("dentista") || pergunta.Contains("exame")) &&
                        (resposta.Contains("mais de 6 meses") || resposta.Contains("mais de 1 ano") ||
                         resposta.Contains("nunca") || resposta.Contains("há muito tempo") ||
                         resposta.Contains("não") || resposta.Contains("mais de 2 anos")))
                    {
                        examNeeded++;
                    }
                }

                // Armazenar informações para análise estatística
                if (!perguntaRelevance.ContainsKey(checkIn.IdPergunta))
                {
                    perguntaRelevance[checkIn.IdPergunta] = 0;
                }
                perguntaRelevance[checkIn.IdPergunta]++;

                if (!respostaDistribution.ContainsKey(checkIn.IdPergunta))
                {
                    respostaDistribution[checkIn.IdPergunta] = new List<int>();
                }
                respostaDistribution[checkIn.IdPergunta].Add(checkIn.IdResposta);
            }

            // Determinar a recomendação mais apropriada baseada nos problemas detectados
            int recommendationIndex = 0; // Valor padrão: limpeza profissional

            // Criar uma lista de problemas detectados com suas contagens e índices de recomendação
            var issues = new List<(int count, int recommendationIndex, int priority)>
            {
                // Formato: (contagem, índice da recomendação, prioridade clínica)
                (painIssues, 4, 1),            // Dor: consulta urgente - prioridade máxima
                (bleedingIssues, 5, 2),        // Sangramento: gengivite
                (bruxismIssues, 6, 3),         // Bruxismo: placa de mordida
                (sensitivityIssues, 3, 4),     // Sensibilidade: pasta especial
                (hygieneIssues, 0, 5),         // Higiene: vamos determinar se é escova (1) ou fio (2) mais adiante
                (dryMouthIssues, 7, 6),        // Boca seca
                (sugarConsumption, 8, 7),      // Consumo de açúcar
                (examNeeded, 9, 8)             // Exame completo
            };

            // Encontrar a contagem máxima de problemas
            int maxCount = issues.Max(i => i.count);

            if (maxCount > 0)
            {
                // Filtrar problemas com a contagem máxima
                var priorityIssues = issues.Where(i => i.count == maxCount).ToList();

                // Se há mais de um problema com a mesma contagem, escolher o de maior prioridade clínica
                var highestPriorityIssue = priorityIssues.OrderBy(i => i.priority).First();

                // Definir o índice de recomendação
                recommendationIndex = highestPriorityIssue.recommendationIndex;

                // Caso especial para problemas de higiene (escolher entre escovação ou fio dental)
                if (recommendationIndex == 0 && hygieneIssues > 0)
                {
                    var escovaMentions = recentCheckIns.Count(c =>
                        c.Perguntas != null &&
                        (c.Perguntas.DsPergunta.ToLower().Contains("escova") ||
                         c.Perguntas.DsPergunta.ToLower().Contains("higiene")));

                    var fioMentions = recentCheckIns.Count(c =>
                        c.Perguntas != null &&
                        c.Perguntas.DsPergunta.ToLower().Contains("fio"));

                    recommendationIndex = escovaMentions >= fioMentions ? 1 : 2;
                }
            }
            else if (recentCheckIns.Count >= 4)
            {
                // Se não detectamos problemas específicos mas temos muitas respostas,
                // recomendar exame bucal completo
                recommendationIndex = 9;
            }

            // Armazenar a recomendação para este paciente
            _patientRecommendationMap[patientId] = recommendationIndex;
        }

        /// <summary>
        /// Mapeia uma combinação de pergunta-resposta para uma das recomendações predefinidas
        /// </summary>
        private void MapQuestionAnswerToRecommendation(int perguntaId, int respostaId)
        {
            var key = (perguntaId, respostaId);

            if (!_recommendationMap.ContainsKey(key))
            {
                // Hash simples para mapear deterministicamente combinações para recomendações
                // Em um sistema real, isso seria baseado em conhecimento de domínio ou padrões
                int recommendationIndex = ((perguntaId * 31) + respostaId) % _commonRecommendations.Count;
                _recommendationMap[key] = recommendationIndex;
            }
        }

        /// <summary>
        /// Determina se um check-in indica um potencial problema dental, considerando o contexto
        /// </summary>
        private bool DetermineIfPotentialIssue(CheckIn checkIn, List<CheckIn> recentCheckIns)
        {
            // Verificar problemas específicos baseados no conteúdo da pergunta e resposta
            if (checkIn.Perguntas != null && checkIn.Respostas != null)
            {
                string pergunta = checkIn.Perguntas.DsPergunta.ToLower();
                string resposta = checkIn.Respostas.DsResposta.ToLower();

                // Dor é sempre um problema potencial
                if (pergunta.Contains("dor") &&
                    (resposta.Contains("sim") || resposta.Contains("forte") || resposta.Contains("constant")))
                {
                    return true;
                }

                // Sangramento gengival é um problema potencial
                if ((pergunta.Contains("gengiva") || pergunta.Contains("sangr")) &&
                    (resposta.Contains("sim") || resposta.Contains("às vezes")))
                {
                    return true;
                }

                // Sensibilidade extrema ou persistente é um problema
                if (pergunta.Contains("sensibilidade") &&
                    (resposta.Contains("forte") || resposta.Contains("muito")))
                {
                    return true;
                }

                // Falta de higiene é um problema
                if ((pergunta.Contains("escova") || pergunta.Contains("fio dental")) &&
                    (resposta.Contains("raramente") || resposta.Contains("nunca")))
                {
                    return true;
                }

                // Bruxismo pode ser um problema
                if (pergunta.Contains("range") && resposta.Contains("sim"))
                {
                    return true;
                }

                // Muito tempo sem consulta é um problema
                if ((pergunta.Contains("consulta") || pergunta.Contains("dentista")) &&
                    (resposta.Contains("mais de 1 ano") || resposta.Contains("nunca")))
                {
                    return true;
                }
            }

            // Lógica de fallback se não pudermos analisar o conteúdo
            bool isOddResponse = checkIn.IdResposta % 2 == 1;
            bool isCriticalQuestion = checkIn.IdPergunta <= 3;

            return isOddResponse && isCriticalQuestion;
        }
                
        /// <summary>
        /// Treina o modelo para fazer previsões contextuais baseadas em histórico de 5 dias
        /// </summary>
        public void TrainContextualModel(IEnumerable<ContextualCheckInData> trainingData)
        {
            if (!trainingData.Any())
            {
                Console.WriteLine("Dados de treinamento contextuais insuficientes. O modelo contextual não será treinado.");
                return;
            }

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Pipeline para processamento de texto agregado
            var pipeline = _mlContext.Transforms.Text
                // Processar perguntas consolidadas
                .FeaturizeText("QuestionsFeatures", nameof(ContextualCheckInData.ConsolidatedQuestions))
                // Processar respostas consolidadas
                .Append(_mlContext.Transforms.Text.FeaturizeText("AnswersFeatures", nameof(ContextualCheckInData.ConsolidatedAnswers)))
                // Converter flags booleanas diretamente para números (evitando MapValueToKey)
                .Append(_mlContext.Transforms.Conversion.ConvertType(
                    outputColumnName: "PainTopicFeature",
                    inputColumnName: nameof(ContextualCheckInData.ContainsPainTopic),
                    DataKind.Single))
                .Append(_mlContext.Transforms.Conversion.ConvertType(
                    outputColumnName: "BleedingTopicFeature",
                    inputColumnName: nameof(ContextualCheckInData.ContainsBleedingTopic),
                    DataKind.Single))
                .Append(_mlContext.Transforms.Conversion.ConvertType(
                    outputColumnName: "SensitivityTopicFeature",
                    inputColumnName: nameof(ContextualCheckInData.ContainsSensitivityTopic),
                    DataKind.Single))
                .Append(_mlContext.Transforms.Conversion.ConvertType(
                    outputColumnName: "BruxismTopicFeature",
                    inputColumnName: nameof(ContextualCheckInData.ContainsBruxismTopic),
                    DataKind.Single))
                // Converter contagem para float
                .Append(_mlContext.Transforms.Conversion.ConvertType(
                    outputColumnName: "CheckInCountFloat",
                    inputColumnName: nameof(ContextualCheckInData.CheckInCount),
                    DataKind.Single))
                // Combinar todas as features
                .Append(_mlContext.Transforms.Concatenate("Features",
                    "QuestionsFeatures", "AnswersFeatures", 
                    "PainTopicFeature", "BleedingTopicFeature", 
                    "SensitivityTopicFeature", "BruxismTopicFeature",
                    "CheckInCountFloat"))
                // Treinar usando FastTree
                .Append(_mlContext.BinaryClassification.Trainers.FastTree(
                    numberOfLeaves: 20,
                    numberOfTrees: 100,
                    minimumExampleCountPerLeaf: 10));

            _contextualModel = pipeline.Fit(dataView);
        }        

        /// <summary>
        /// Prever problemas baseado em análise contextual de check-ins recentes
        /// </summary>
        public ContextualIssuePrediction PredictFromContext(ContextualCheckInData contextData)
        {
            if (_contextualModel == null)
                throw new InvalidOperationException("Modelo contextual não está treinado.");

            var engine = _mlContext.Model.CreatePredictionEngine<ContextualCheckInData, ContextualIssuePrediction>(_contextualModel);
            return engine.Predict(contextData);
        }

        /// <summary>
        /// Obter recomendação consolidada para um paciente com base em seu histórico recente
        /// </summary>
        public async Task<string> GetPatientRecommendationAsync(int pacienteId)
        {
            // Verificar se já temos uma recomendação mapeada para este paciente
            if (_patientRecommendationMap.TryGetValue(pacienteId, out int recommendationIndex))
            {
                return _commonRecommendations[recommendationIndex];
            }

            // Caso contrário, buscar os check-ins recentes e analisar
            var lastCheckIn = await _dataContext.CheckIn
                .Where(c => c.IdPaciente == pacienteId)
                .OrderByDescending(c => c.DtCheckIn)
                .FirstOrDefaultAsync();

            if (lastCheckIn == null)
            {
                return "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.";
            }

            // Obter check-ins dos últimos 5 dias
            var recentCheckIns = await _dataContext.CheckIn
                .Include(c => c.Perguntas)
                .Include(c => c.Respostas)
                .Where(c => c.IdPaciente == pacienteId && c.DtCheckIn >= lastCheckIn.DtCheckIn.AddDays(-5))
                .OrderByDescending(c => c.DtCheckIn)
                .ToListAsync();

            if (recentCheckIns.Count == 0)
            {
                return "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.";
            }

            try
            {
                // Primeiro analisamos os dados para determinar se há um padrão específico
                AnalyzePatientHistoryAndCreateRecommendation(pacienteId, recentCheckIns);

                if (_patientRecommendationMap.TryGetValue(pacienteId, out recommendationIndex))
                {
                    return _commonRecommendations[recommendationIndex];
                }

                // Se não conseguimos determinar um padrão específico, usar o serviço de IA 
                // para gerar uma recomendação consolidada baseada em todos os check-ins
                return await _generativeAI.GenerateDentalRecommendation(recentCheckIns);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar recomendação para paciente {pacienteId}: {ex.Message}");
                return "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.";
            }
        }

        /// <summary>
        /// Obter recomendação com base em uma combinação específica de pergunta-resposta
        /// Esta função é mantida para compatibilidade com código existente
        /// </summary>
        public async Task<string> GetRecommendationAsync(int perguntaId, int respostaId)
        {
            // Primeiro verificar se temos um mapeamento para esta combinação pergunta-resposta
            if (_recommendationMap.TryGetValue((perguntaId, respostaId), out int recommendationIndex))
            {
                return _commonRecommendations[recommendationIndex];
            }

            // Se não existe mapeamento, usar método padrão para gerar um
            try
            {
                var pergunta = await _dataContext.Perguntas.FindAsync(perguntaId);
                var resposta = await _dataContext.Respostas.FindAsync(respostaId);

                if (pergunta == null || resposta == null)
                {
                    return "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.";
                }

                // Obter todos os check-ins com esta pergunta/resposta
                var similarCheckIns = await _dataContext.CheckIn
                    .Include(c => c.Perguntas)
                    .Include(c => c.Respostas)
                    .Where(c => c.IdPergunta == perguntaId && c.IdResposta == respostaId)
                    .ToListAsync();

                if (similarCheckIns.Count > 0)
                {
                    // Usar o serviço de IA para uma recomendação mais detalhada
                    return await _generativeAI.GenerateDentalRecommendation(similarCheckIns);
                }
                else
                {
                    // Sem check-ins similares, usar uma recomendação padrão
                    return _commonRecommendations[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar recomendação para pergunta {perguntaId} e resposta {respostaId}: {ex.Message}");
                return "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.";
            }
        }

        /// <summary>
        /// Prever uma recomendação para um paciente baseada em seu histórico de 5 dias
        /// </summary>
        public async Task<PredictionResult> PredictRecommendationForPatient(int pacienteId)
        {
            // Buscar paciente para ter as informações básicas
            var paciente = await _dataContext.Paciente.FindAsync(pacienteId);
            if (paciente == null)
            {
                throw new ArgumentException($"Paciente com ID {pacienteId} não encontrado");
            }

            // Encontrar a data de check-in mais recente do paciente
            var lastCheckIn = await _dataContext.CheckIn
                .Where(c => c.IdPaciente == pacienteId)
                .OrderByDescending(c => c.DtCheckIn)
                .FirstOrDefaultAsync();

            if (lastCheckIn == null)
            {
                return new PredictionResult
                {
                    CheckInDate = DateTime.Now,
                    Question = "Nenhum check-in encontrado",
                    Answer = "Nenhuma resposta registrada",
                    PotentialIssue = false,
                    Confidence = 0.0f,
                    Recommendation = "Recomendamos agendar sua primeira consulta odontológica para avaliar sua saúde bucal.",
                    PatientName = paciente.NmPaciente,
                    PatientId = pacienteId,
                    TotalCheckInsAnalyzed = 0
                };
            }

            // Obter check-ins dos últimos 5 dias
            var recentCheckIns = await _dataContext.CheckIn
                .Include(c => c.Perguntas)
                .Include(c => c.Respostas)
                .Where(c => c.IdPaciente == pacienteId && c.DtCheckIn >= lastCheckIn.DtCheckIn.AddDays(-5))
                .OrderByDescending(c => c.DtCheckIn)
                .ToListAsync();

            if (recentCheckIns.Count == 0)
            {
                return new PredictionResult
                {
                    CheckInDate = lastCheckIn.DtCheckIn,
                    Question = "Nenhum check-in recente",
                    Answer = "Nenhuma resposta recente",
                    PotentialIssue = false,
                    Confidence = 0.0f,
                    Recommendation = "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.",
                    PatientName = paciente.NmPaciente,
                    PatientId = pacienteId,
                    TotalCheckInsAnalyzed = 0
                };
            }

            // Analisar o histórico para determinar a recomendação baseada em regras
            AnalyzePatientHistoryAndCreateRecommendation(pacienteId, recentCheckIns);

            // Criar dados contextuais consolidando todos os check-ins recentes
            var contextualData = new ContextualCheckInData
            {
                PacienteId = pacienteId,
                CheckInCount = recentCheckIns.Count,
                MostRecentDate = recentCheckIns.First().DtCheckIn,
                OldestDate = recentCheckIns.Last().DtCheckIn,

                // Consolidar todas as perguntas e respostas em textos únicos para análise contextual
                ConsolidatedQuestions = string.Join(" | ", recentCheckIns
                    .Where(c => c.Perguntas != null)
                    .Select(c => c.Perguntas.DsPergunta)),

                ConsolidatedAnswers = string.Join(" | ", recentCheckIns
                    .Where(c => c.Respostas != null)
                    .Select(c => c.Respostas.DsResposta)),

                // Detectar tópicos relevantes no conjunto completo
                ContainsPainTopic = recentCheckIns.Any(c =>
                    c.Perguntas?.DsPergunta.ToLower().Contains("dor") == true),

                ContainsBleedingTopic = recentCheckIns.Any(c =>
                    c.Perguntas?.DsPergunta.ToLower().Contains("sangr") == true ||
                    c.Perguntas?.DsPergunta.ToLower().Contains("gengiva") == true),

                ContainsSensitivityTopic = recentCheckIns.Any(c =>
                    c.Perguntas?.DsPergunta.ToLower().Contains("sensibilidade") == true),

                ContainsBruxismTopic = recentCheckIns.Any(c =>
                    c.Perguntas?.DsPergunta.ToLower().Contains("range") == true ||
                    c.Perguntas?.DsPergunta.ToLower().Contains("mandíbula") == true)
            };

            // Determinar se há problema potencial usando o modelo contextual
            bool hasPotentialIssue = false;
            float confidence = 0.0f;

            try
            {
                if (_contextualModel != null)
                {
                    var contextualPrediction = PredictFromContext(contextualData);
                    hasPotentialIssue = contextualPrediction.PredictedIssue;
                    confidence = contextualPrediction.Probability;
                }
                else
                {
                    // Se modelo contextual não estiver disponível, verificar cada check-in individualmente
                    foreach (var checkIn in recentCheckIns)
                    {
                        if (checkIn.Perguntas != null && checkIn.Respostas != null)
                        {
                            bool isIssue = DetermineIfPotentialIssue(checkIn, recentCheckIns);
                            if (isIssue)
                            {
                                hasPotentialIssue = true;
                                confidence = Math.Max(confidence, 0.7f); // Confiança padrão para detecção baseada em regras
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na predição contextual: {ex.Message}. Usando método de regras.");

                // Se falhar a predição contextual, usar método baseado em regras
                foreach (var checkIn in recentCheckIns)
                {
                    if (checkIn.Perguntas != null && checkIn.Respostas != null)
                    {
                        bool isIssue = DetermineIfPotentialIssue(checkIn, recentCheckIns);
                        if (isIssue)
                        {
                            hasPotentialIssue = true;
                            confidence = Math.Max(confidence, 0.7f);
                        }
                    }
                }
            }

            // Obter a recomendação para o paciente baseada em todo seu histórico
            string recommendation = await GetPatientRecommendationAsync(pacienteId);

            // Montar o resultado usando dados consolidados
            return new PredictionResult
            {
                CheckInDate = lastCheckIn.DtCheckIn,
                Question = "Análise dos últimos 5 dias de check-ins",
                Answer = $"Analisados {recentCheckIns.Count} check-ins recentes",
                PotentialIssue = hasPotentialIssue,
                Confidence = confidence,
                Recommendation = recommendation,
                PatientName = paciente.NmPaciente,
                PatientId = pacienteId,
                TotalCheckInsAnalyzed = recentCheckIns.Count
            };
        }

        

        /// <summary>
        /// Gera uma recomendação personalizada para um paciente usando IA generativa baseada nos últimos 5 dias de check-ins
        /// </summary>
        public async Task<PredictionResult> GenerateAIRecommendationForPatient(int pacienteId)
        {
            // Buscar paciente para ter as informações básicas
            var paciente = await _dataContext.Paciente.FindAsync(pacienteId);
            if (paciente == null)
            {
                throw new ArgumentException($"Paciente com ID {pacienteId} não encontrado");
            }

            // Encontrar a data de check-in mais recente do paciente
            var lastCheckIn = await _dataContext.CheckIn
                .Where(c => c.IdPaciente == pacienteId)
                .OrderByDescending(c => c.DtCheckIn)
                .FirstOrDefaultAsync();

            if (lastCheckIn == null)
            {
                return new PredictionResult
                {
                    CheckInDate = DateTime.Now,
                    Question = "Nenhum check-in encontrado",
                    Answer = "Nenhuma resposta registrada",
                    PotentialIssue = false,
                    Confidence = 0.0f,
                    Recommendation = "Recomendamos agendar sua primeira consulta odontológica para avaliação inicial.",
                    PatientName = paciente.NmPaciente,
                    PatientId = pacienteId,
                    TotalCheckInsAnalyzed = 0
                };
            }

            // Obter check-ins dos últimos 5 dias
            var recentCheckIns = await _dataContext.CheckIn
                .Include(c => c.Perguntas)
                .Include(c => c.Respostas)
                .Where(c => c.IdPaciente == pacienteId && c.DtCheckIn >= lastCheckIn.DtCheckIn.AddDays(-5))
                .OrderByDescending(c => c.DtCheckIn)
                .ToListAsync();

            if (recentCheckIns.Count == 0)
            {
                return new PredictionResult
                {
                    CheckInDate = lastCheckIn.DtCheckIn,
                    Question = "Nenhum check-in recente",
                    Answer = "Nenhuma resposta recente",
                    PotentialIssue = false,
                    Confidence = 0.0f,
                    Recommendation = "Continue mantendo sua saúde bucal com escovação regular e visitas ao dentista.",
                    PatientName = paciente.NmPaciente,
                    PatientId = pacienteId,
                    TotalCheckInsAnalyzed = 0
                };
            }

            // Determinar possíveis problemas usando o método existente (para manter consistência)
            bool hasPotentialIssue = recentCheckIns.Any(c => DetermineIfPotentialIssue(c, recentCheckIns));

            // Obter recomendação personalizada da IA generativa
            string aiRecommendation;
            try
            {
                aiRecommendation = await _generativeAI.GenerateDentalRecommendation(recentCheckIns);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na geração de recomendação por IA: {ex.Message}. Usando recomendação padrão.");
                aiRecommendation = "Recomendamos agendar uma consulta para avaliação profissional com base no seu histórico recente.";
            }

            // Montar o resultado usando dados consolidados
            return new PredictionResult
            {
                CheckInDate = lastCheckIn.DtCheckIn,
                Question = "Análise de IA dos últimos 5 dias de check-ins",
                Answer = $"Analisados {recentCheckIns.Count} check-ins recentes com Inteligência Artificial",
                PotentialIssue = hasPotentialIssue,
                Confidence = hasPotentialIssue ? 0.85f : 0.15f, // Confiança arbitrária para IA generativa
                Recommendation = aiRecommendation,
                PatientName = paciente.NmPaciente,
                PatientId = pacienteId,
                TotalCheckInsAnalyzed = recentCheckIns.Count
            };
        }
    }
}