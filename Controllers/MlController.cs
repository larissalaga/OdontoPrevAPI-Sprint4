using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.MlModels;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLController : ControllerBase
    {
        private readonly MlService _mlService;
        private readonly ICheckInRepository _checkInRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPerguntasRepository _perguntasRepository;
        private readonly IRespostasRepository _respostasRepository;

        public MLController(
            MlService mlService,
            ICheckInRepository checkInRepository,
            IPacienteRepository pacienteRepository,
            IPerguntasRepository perguntasRepository,
            IRespostasRepository respostasRepository)
        {
            _mlService = mlService;
            _checkInRepository = checkInRepository;
            _pacienteRepository = pacienteRepository;
            _perguntasRepository = perguntasRepository;
            _respostasRepository = respostasRepository;
        }

        /// <summary>
        /// Treina o modelo ML com dados de check-in existentes na janela de 5 dias
        /// </summary>
        [HttpPost("ml-train/paciente")]
        [SwaggerOperation(
            Summary = "Treinar modelos ML (não generativa)",
            Description = "Treina os modelos de machine learning (não generativa) com análise contextual de 5 dias por paciente")]
        [SwaggerResponse(200, "Modelos treinados com sucesso")]
        [SwaggerResponse(500, "Erro ao treinar modelos")]
        public async Task<IActionResult> TrainModel()
        {
            try
            {
                await _mlService.TrainModelFromDatabase();
                return Ok(new
                {
                    message = "Modelos treinados com sucesso",
                    details = "Treinamento realizado com análise contextual de 5 dias"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao treinar modelos: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém uma recomendação personalizada para um paciente com base em seu histórico de 5 dias
        /// </summary>
        [HttpGet("ml-recommendation/paciente/id/{id}")]
        [SwaggerOperation(
            Summary = "Obter recomendação para paciente, via ML (não generativa)",
            Description = "Analisa o histórico de 5 dias de check-ins do paciente e retorna uma recomendação personalizada")]
        [SwaggerResponse(200, "Recomendação gerada com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar recomendação")]
        public async Task<IActionResult> GetRecommendationForPatient(int id)
        {
            try
            {
                // Verificar se o paciente existe
                var paciente = await _pacienteRepository.GetById(id);
                if (paciente == null)
                {
                    return NotFound($"Paciente com ID {id} não encontrado");
                }

                // Obter previsão e recomendação para o paciente com base nos últimos 5 dias
                var prediction = await _mlService.PredictRecommendationForPatient(id);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente,
                        cpf = paciente.NrCpf
                    },
                    recomendacao = prediction.Recommendation,
                    potencialProblema = prediction.PotentialIssue,
                    periodoAnalise = $"Últimos {prediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = prediction.CheckInDate,
                    confianca = prediction.Confidence,
                    checkInsAnalisados = prediction.TotalCheckInsAnalyzed,
                    analiseContexual = true
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar recomendação: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém previsões de saúde bucal para um paciente específico baseadas em contexto de 5 dias
        /// </summary>
        [HttpGet("ml-recommendation/paciente/cpf/{cpf}")]
        [SwaggerOperation(
            Summary = "Obter previsões para paciente por CPF, via ML (não generativa)",
            Description = "Analisa os check-ins dos últimos 5 dias de um paciente e retorna previsões contextuais")]
        [SwaggerResponse(200, "Previsões geradas com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar previsões")]
        public async Task<IActionResult> GetRecommendationForPatientByCPF(string cpf)
        {
            try
            {
                var paciente = await _pacienteRepository.GetByNrCpf(cpf);
                if (paciente == null)
                {
                    return NotFound($"Paciente com CPF {cpf} não encontrado");
                }

                // Obter previsão e recomendação para o paciente com base nos últimos 5 dias
                var prediction = await _mlService.PredictRecommendationForPatient(paciente.IdPaciente);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente,
                        cpf = paciente.NrCpf
                    },
                    recomendacao = prediction.Recommendation,
                    potencialProblema = prediction.PotentialIssue,
                    periodoAnalise = $"Últimos {prediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = prediction.CheckInDate,
                    confianca = prediction.Confidence,
                    checkInsAnalisados = prediction.TotalCheckInsAnalyzed,
                    analiseContexual = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar previsões: {ex.Message}");
            }
        }
        /// <summary>
        /// Obtém uma recomendação personalizada usando IA generativa para um paciente
        /// </summary>
        [HttpGet("ai-recommendation/paciente/id/{id}")]
        [SwaggerOperation(
            Summary = "Obter recomendação de IA para paciente",
            Description = "Usa IA generativa para analisar o histórico de 5 dias de check-ins do paciente")]
        [SwaggerResponse(200, "Recomendação gerada com sucesso pela IA")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar recomendação")]
        public async Task<IActionResult> GetAIRecommendationForPatient(int id)
        {
            try
            {
                // Verificar se o paciente existe
                var paciente = await _pacienteRepository.GetById(id);
                if (paciente == null)
                {
                    return NotFound($"Paciente com ID {id} não encontrado");
                }

                // Obter recomendação gerada por IA para o paciente
                var prediction = await _mlService.GenerateAIRecommendationForPatient(id);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente
                    },
                    periodoAnalise = $"Últimos {prediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = prediction.CheckInDate,
                    recomendacao = prediction.Recommendation,
                    potencialProblema = prediction.PotentialIssue,
                    fonteDaRecomendacao = "Inteligência Artificial Generativa",
                    checkInsAnalisados = prediction.TotalCheckInsAnalyzed
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar recomendação por IA: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém uma recomendação personalizada usando IA generativa para um paciente
        /// </summary>
        [HttpGet("ai-recommendation/paciente/cpf/{cpf}")]
        [SwaggerOperation(
            Summary = "Obter recomendação de IA para paciente",
            Description = "Usa IA generativa para analisar o histórico de 5 dias de check-ins do paciente")]
        [SwaggerResponse(200, "Recomendação gerada com sucesso pela IA")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar recomendação")]
        public async Task<IActionResult> GetAIRecommendationForPatientByCPF(string cpf)
        {
            try
            {
                // Verificar se o paciente existe
                var paciente = await _pacienteRepository.GetByNrCpf(cpf);
                if (paciente == null)
                {
                    return NotFound($"Paciente com CPF {cpf} não encontrado");
                }

                // Obter recomendação gerada por IA para o paciente
                var prediction = await _mlService.GenerateAIRecommendationForPatient(paciente.IdPaciente);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente
                    },
                    periodoAnalise = $"Últimos {prediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = prediction.CheckInDate,
                    recomendacao = prediction.Recommendation,
                    potencialProblema = prediction.PotentialIssue,
                    fonteDaRecomendacao = "Inteligência Artificial Generativa",
                    checkInsAnalisados = prediction.TotalCheckInsAnalyzed
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar recomendação por IA: {ex.Message}");
            }
        }

        /// <summary>
        /// Compara recomendações de ML e IA generativa para um paciente
        /// </summary>
        [HttpGet("compare-recommendations/paciente/id/{id}")]
        [SwaggerOperation(
            Summary = "Comparar recomendações ML vs IA",
            Description = "Compara recomendações geradas por Machine Learning e IA Generativa")]
        [SwaggerResponse(200, "Comparação gerada com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar comparação")]
        public async Task<IActionResult> CompareRecommendations(int id)
        {
            try
            {
                // Verificar se o paciente existe
                var paciente = await _pacienteRepository.GetById(id);
                if (paciente == null)
                {
                    return NotFound($"Paciente com ID {id} não encontrado");
                }

                // Obter recomendações usando ambas abordagens
                var mlPrediction = await _mlService.PredictRecommendationForPatient(id);
                var aiPrediction = await _mlService.GenerateAIRecommendationForPatient(id);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente
                    },
                    periodoAnalise = $"Últimos {mlPrediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = mlPrediction.CheckInDate,
                    machine_learning = new
                    {
                        recomendacao = mlPrediction.Recommendation,
                        potencialProblema = mlPrediction.PotentialIssue,
                        confianca = mlPrediction.Confidence
                    },
                    inteligencia_artificial = new
                    {
                        recomendacao = aiPrediction.Recommendation,
                        potencialProblema = aiPrediction.PotentialIssue,
                        confianca = aiPrediction.Confidence
                    },
                    checkInsAnalisados = mlPrediction.TotalCheckInsAnalyzed
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao comparar recomendações: {ex.Message}");
            }
        }

        /// <summary>
        /// Compara recomendações de ML e IA generativa para um paciente
        /// </summary>
        [HttpGet("compare-recommendations/paciente/cpf/{cpf}")]
        [SwaggerOperation(
            Summary = "Comparar recomendações ML vs IA",
            Description = "Compara recomendações geradas por Machine Learning e IA Generativa")]
        [SwaggerResponse(200, "Comparação gerada com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar comparação")]
        public async Task<IActionResult> CompareRecommendationsByCPF(string cpf)
        {
            try
            {
                // Verificar se o paciente existe
                var paciente = await _pacienteRepository.GetByNrCpf(cpf);
                if (paciente == null)
                {
                    return NotFound($"Paciente com CPF {cpf} não encontrado");
                }

                // Obter recomendações usando ambas abordagens
                var mlPrediction = await _mlService.PredictRecommendationForPatient(paciente.IdPaciente);
                var aiPrediction = await _mlService.GenerateAIRecommendationForPatient(paciente.IdPaciente);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente
                    },
                    periodoAnalise = $"Últimos {mlPrediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = mlPrediction.CheckInDate,
                    machine_learning = new
                    {
                        recomendacao = mlPrediction.Recommendation,
                        potencialProblema = mlPrediction.PotentialIssue,
                        confianca = mlPrediction.Confidence
                    },
                    inteligencia_artificial = new
                    {
                        recomendacao = aiPrediction.Recommendation,
                        potencialProblema = aiPrediction.PotentialIssue,
                        confianca = aiPrediction.Confidence
                    },
                    checkInsAnalisados = mlPrediction.TotalCheckInsAnalyzed
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao comparar recomendações: {ex.Message}");
            }
        }
        
    }
}