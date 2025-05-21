using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar relações entre Paciente e Dentista.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteDentistaController : ControllerBase
    {
        private readonly IPacienteDentistaRepository _pacienteDentistaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IDentistaRepository _dentistaRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PacienteDentistaController"/>.
        /// </summary>
        /// <param name="pacienteDentistaRepository">O repositório para relações Paciente-Dentista.</param>
        /// <param name="pacienteRepository">O repositório para entidades Paciente.</param>
        /// <param name="dentistaRepository">O repositório para entidades Dentista.</param>
        public PacienteDentistaController(
            IPacienteDentistaRepository pacienteDentistaRepository,
            IPacienteRepository pacienteRepository,
            IDentistaRepository dentistaRepository)
        {
            _pacienteDentistaRepository = pacienteDentistaRepository;
            _pacienteRepository = pacienteRepository;
            _dentistaRepository = dentistaRepository;
        }

        /// <summary>
        /// Cria uma nova relação entre Paciente e Dentista.
        /// </summary>
        /// <param name="idDentista">O ID do Dentista.</param>
        /// <param name="idPaciente">O ID do Paciente.</param>
        /// <returns>A relação Paciente-Dentista criada.</returns>
        [HttpPost("new")]
        [SwaggerOperation(Summary = "Cria uma nova relação Paciente-Dentista", Description = "Cria uma nova relação entre um Paciente e um Dentista utilizando seus IDs.")]
        [SwaggerResponse(201, "Relação Paciente-Dentista criada com sucesso")]
        [SwaggerResponse(400, "IDs inválidos")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> Create([FromQuery] int idDentista, [FromQuery] int idPaciente)
        {
            try
            {
                var relationship = await _pacienteDentistaRepository.Create(idDentista, idPaciente);
                return CreatedAtAction(nameof(Create), new { idDentista, idPaciente }, relationship);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deleta uma relação entre Paciente e Dentista.
        /// </summary>
        /// <param name="dsCro">O CRO do Dentista.</param>
        /// <param name="nrCpf">O CPF do Paciente.</param>
        /// <returns>Confirmação de exclusão.</returns>
        [HttpDelete("delete")]
        [SwaggerOperation(Summary = "Deleta uma relação Paciente-Dentista", Description = "Deleta uma relação entre um Paciente e um Dentista utilizando CRO e CPF.")]
        [SwaggerResponse(200, "Relação Paciente-Dentista deletada com sucesso")]
        [SwaggerResponse(404, "Relação não encontrada")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> Delete([FromQuery] string dsCro, [FromQuery] string nrCpf)
        {
            try
            {
                var result = await _pacienteDentistaRepository.Delete(dsCro, nrCpf);
                if (result)
                {
                    return Ok("Relação Paciente-Dentista deletada com sucesso.");
                }
                else
                {
                    return NotFound("Relação Paciente-Dentista não encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


