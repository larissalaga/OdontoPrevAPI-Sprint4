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
    public class MlController : ControllerBase
    {
        private readonly MlService _mlService;
        private readonly ICheckInRepository _checkInRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPerguntasRepository _perguntasRepository;
        private readonly IRespostasRepository _respostasRepository;

        public MlController(
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
        [HttpPost("train")]
        [SwaggerOperation(
            Summary = "Treinar modelos ML",
            Description = "Treina os modelos de machine learning com análise contextual de 5 dias por paciente")]
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
        [HttpGet("recommendation/paciente/{id}")]
        [SwaggerOperation(
            Summary = "Obter recomendação para paciente",
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
                        nome = paciente.NmPaciente
                    },
                    periodoAnalise = $"Últimos {prediction.TotalCheckInsAnalyzed} check-ins (até 5 dias)",
                    dataUltimoCheckIn = prediction.CheckInDate,
                    recomendacao = prediction.Recommendation,
                    potencialProblema = prediction.PotentialIssue,
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
        /// Obtém um relatório detalhado da saúde bucal de um paciente baseado nos últimos 5 dias
        /// </summary>
        [HttpGet("paciente/{id}/relatorio")]
        [SwaggerOperation(
            Summary = "Obter relatório de saúde bucal",
            Description = "Obtém um relatório detalhado da saúde bucal do paciente com base em seus check-ins dos últimos 5 dias")]
        [SwaggerResponse(200, "Relatório gerado com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar relatório")]
        public async Task<IActionResult> GetPatientDentalReport(int id)
        {
            try
            {
                // Verificar se o paciente existe
                var paciente = await _pacienteRepository.GetById(id);
                if (paciente == null)
                {
                    return NotFound($"Paciente com ID {id} não encontrado");
                }

                // Obter relatório detalhado dos últimos 5 dias
                var summary = await _mlService.GetPatientDentalSummary(id);

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente
                    },
                    resumo = new
                    {
                        totalCheckIns = summary.TotalCheckIns,
                        problemasIdentificados = summary.ProblemasIdentificados,
                        confiancaMedia = summary.ConfiancaMedia,
                        periodoAnalise = "Últimos 5 dias",
                        recomendacaoPrincipal = summary.RecomendacaoPrincipal
                    },
                    detalhes = summary.DetalhesAnalise
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar relatório: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém previsões de saúde bucal para um paciente específico baseadas em contexto de 5 dias
        /// </summary>
        [HttpGet("predict/{cpf}")]
        [SwaggerOperation(
            Summary = "Obter previsões para paciente por CPF",
            Description = "Analisa os check-ins dos últimos 5 dias de um paciente e retorna previsões contextuais")]
        [SwaggerResponse(200, "Previsões geradas com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro ao gerar previsões")]
        public async Task<IActionResult> GetPredictions(string cpf)
        {
            try
            {
                var paciente = await _pacienteRepository.GetByNrCpf(cpf);
                if (paciente == null)
                {
                    return NotFound($"Paciente com CPF {cpf} não encontrado");
                }

                var results = await _mlService.AnalyzePatientCheckIns(paciente.IdPaciente);

                if (results.Count == 0)
                {
                    return Ok(new
                    {
                        paciente = new
                        {
                            id = paciente.IdPaciente,
                            nome = paciente.NmPaciente,
                            cpf = paciente.NrCpf
                        },
                        mensagem = "Não foram encontrados check-ins recentes para este paciente."
                    });
                }

                // Primeiro resultado é a análise contextual consolidada
                var contextualAnalysis = results.First();

                // Os demais são os check-ins individuais
                var individualCheckIns = results.Skip(1).ToList();

                // Agrupar os check-ins individuais por data para melhor visualização
                var groupedCheckIns = individualCheckIns
                    .GroupBy(r => r.CheckInDate.Date)
                    .Select(g => new {
                        data = g.Key.ToString("dd/MM/yyyy"),
                        checkIns = g.Select(r => new {
                            pergunta = r.Question,
                            resposta = r.Answer
                        }).ToList()
                    })
                    .ToList();

                // Resumo dos problemas potenciais
                var problemSummary = results
                    .Where(r => r.PotentialIssue)
                    .GroupBy(r => r.Recommendation)
                    .Select(g => new {
                        recomendacao = g.Key,
                        ocorrencias = g.Count(),
                        confiancaMedia = g.Average(r => r.Confidence)
                    })
                    .OrderByDescending(x => x.ocorrencias)
                    .ToList();

                return Ok(new
                {
                    paciente = new
                    {
                        id = paciente.IdPaciente,
                        nome = paciente.NmPaciente,
                        cpf = paciente.NrCpf
                    },
                    analiseContextual = new
                    {
                        periodoAnalisado = "Últimos 5 dias",
                        totalCheckIns = individualCheckIns.Count,
                        potencialProblema = contextualAnalysis.PotentialIssue,
                        confianca = contextualAnalysis.Confidence,
                        recomendacao = contextualAnalysis.Recommendation
                    },
                    estatisticas = new
                    {
                        totalCheckIns = individualCheckIns.Count,
                        problemasDetectados = individualCheckIns.Count(r => r.PotentialIssue),
                        porcentagemProblemas = individualCheckIns.Count > 0
                            ? Math.Round(100.0 * individualCheckIns.Count(r => r.PotentialIssue) / individualCheckIns.Count, 1)
                            : 0
                    },
                    resumoProblemas = problemSummary,
                    checkInsPorData = groupedCheckIns
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao gerar previsões: {ex.Message}");
            }
        }
    }
}