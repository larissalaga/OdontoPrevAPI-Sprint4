using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar rela��es entre Paciente e Dentista.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteDentistaController : ControllerBase
    {
        private readonly IPacienteDentistaRepository _pacienteDentistaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IDentistaRepository _dentistaRepository;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe <see cref="PacienteDentistaController"/>.
        /// </summary>
        /// <param name="pacienteDentistaRepository">O reposit�rio para rela��es Paciente-Dentista.</param>
        /// <param name="pacienteRepository">O reposit�rio para entidades Paciente.</param>
        /// <param name="dentistaRepository">O reposit�rio para entidades Dentista.</param>
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
        /// Cria uma nova rela��o entre Paciente e Dentista.
        /// </summary>
        /// <param name="idDentista">O ID do Dentista.</param>
        /// <param name="idPaciente">O ID do Paciente.</param>
        /// <returns>A rela��o Paciente-Dentista criada.</returns>
        [HttpPost("new")]
        [SwaggerOperation(Summary = "Cria uma nova rela��o Paciente-Dentista", Description = "Cria uma nova rela��o entre um Paciente e um Dentista utilizando seus IDs.")]
        [SwaggerResponse(201, "Rela��o Paciente-Dentista criada com sucesso")]
        [SwaggerResponse(400, "IDs inv�lidos")]
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
        /// Deleta uma rela��o entre Paciente e Dentista.
        /// </summary>
        /// <param name="dsCro">O CRO do Dentista.</param>
        /// <param name="nrCpf">O CPF do Paciente.</param>
        /// <returns>Confirma��o de exclus�o.</returns>
        [HttpDelete("delete")]
        [SwaggerOperation(Summary = "Deleta uma rela��o Paciente-Dentista", Description = "Deleta uma rela��o entre um Paciente e um Dentista utilizando CRO e CPF.")]
        [SwaggerResponse(200, "Rela��o Paciente-Dentista deletada com sucesso")]
        [SwaggerResponse(404, "Rela��o n�o encontrada")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> Delete([FromQuery] string dsCro, [FromQuery] string nrCpf)
        {
            try
            {
                var result = await _pacienteDentistaRepository.Delete(dsCro, nrCpf);
                if (result)
                {
                    return Ok("Rela��o Paciente-Dentista deletada com sucesso.");
                }
                else
                {
                    return NotFound("Rela��o Paciente-Dentista n�o encontrada.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


