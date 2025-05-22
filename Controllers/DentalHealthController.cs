using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.MlModels;
using OdontoPrevAPI.Models;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentalHealthController : ControllerBase
    {
        private readonly MlService _mlService;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ICheckInRepository _checkInRepository;

        public DentalHealthController(
            MlService mlService,
            IPacienteRepository pacienteRepository,
            ICheckInRepository checkInRepository)
        {
            _mlService = mlService;
            _pacienteRepository = pacienteRepository;
            _checkInRepository = checkInRepository;
        }

        [HttpGet("analyze/{cpf}")]
        [SwaggerOperation(
            Summary = "Analisa a saúde bucal do paciente",
            Description = "Analisa os check-ins do paciente usando ML.NET e fornece recomendações")]
        [SwaggerResponse(200, "Análise de saúde bucal gerada com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> AnalyzePatientHealth(string cpf)
        {
            try
            {
                var paciente = await _pacienteRepository.GetByNrCpf(cpf);
                if (paciente == null)
                {
                    return NotFound($"Paciente com CPF {cpf} não encontrado.");
                }

                var results = await _mlService.AnalyzePatientCheckIns(paciente.IdPaciente);

                return Ok(new
                {
                    paciente = new
                    {
                        nome = paciente.NmPaciente,
                        cpf = paciente.NrCpf
                    },
                    analise = results,
                    resumo = new
                    {
                        totalIssues = results.Count(r => r.PotentialIssue),
                        recomendacaoPrincipal = results
                            .OrderByDescending(r => r.Confidence)
                            .FirstOrDefault(r => r.PotentialIssue)?.Recommendation ?? "Continue com os cuidados regulares de saúde bucal."
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao analisar saúde bucal: {ex.Message}");
            }
        }

        [HttpPost("train")]
        [SwaggerOperation(
            Summary = "Treina o modelo de ML",
            Description = "Treina o modelo de ML com os dados históricos de check-ins")]
        [SwaggerResponse(200, "Modelo treinado com sucesso")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> TrainModel()
        {
            try
            {
                await _mlService.TrainModelFromDatabase();
                return Ok(new { message = "Modelo de ML treinado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao treinar modelo: {ex.Message}");
            }
        }
    }
}