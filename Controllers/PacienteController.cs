using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar entidades Paciente.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPlanoRepository _planoRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PacienteController"/>.
        /// </summary>
        /// <param name="pacienteRepository">O repositório para entidades Paciente.</param>
        public PacienteController(
            IPacienteRepository pacienteRepository
            , IPlanoRepository planoRepository)
        {
            _pacienteRepository = pacienteRepository;
            _planoRepository = planoRepository;

        }

        /// <summary>
        /// Obtém todas as entidades Paciente.
        /// </summary>
        /// <returns>Uma lista de entidades Paciente.</returns>
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Obtém todas as entidades Paciente", Description = "Retorna uma lista de todas as entidades Paciente.")]
        [SwaggerResponse(200, "Retorna a lista de entidades Paciente")]
        [SwaggerResponse(500, "Nenhum paciente encontrado")]
        public async Task<IActionResult> GetAllPacientes()
        {
            var pacientes = await _pacienteRepository.GetAll();
            return Ok(pacientes);
        }

        /// <summary>
        /// Obtém uma entidade Paciente pelo CPF.
        /// </summary>
        /// <param name="nrCpf">O CPF do Paciente.</param>
        /// <returns>A entidade Paciente.</returns>
        [HttpGet("cpf/{nrCpf}")]
        [SwaggerOperation(Summary = "Obtém uma entidade Paciente pelo CPF", Description = "Retorna uma entidade Paciente pelo seu CPF.")]
        [SwaggerResponse(200, "Retorna a entidade Paciente")]
        [SwaggerResponse(500, "Paciente não encontrado")]
        public async Task<IActionResult> GetPacienteByCpf(string nrCpf)
        {
            // Remove caracteres especiais do CPF
            var cpf = Tools.StringTools.OnlyNumbers(nrCpf);
            var paciente = await _pacienteRepository.GetByNrCpf(cpf);
            return Ok(paciente);
        }

        /// <summary>
        /// Obtém uma entidade Paciente pelo ID.
        /// </summary>
        /// <param name="id">O ID do Paciente.</param>
        /// <returns>A entidade Paciente.</returns>
        [HttpGet("id/{id}")]
        [SwaggerOperation(Summary = "Obtém uma entidade Paciente pelo ID", Description = "Retorna uma entidade Paciente pelo seu ID.")]
        [SwaggerResponse(200, "Retorna a entidade Paciente")]
        [SwaggerResponse(500, "Paciente não encontrado")]
        public async Task<IActionResult> GetPacienteById(int id)
        {
            var paciente = await _pacienteRepository.GetById(id);
            return Ok(paciente);
        }

        /// <summary>
        /// Cria uma nova entidade Paciente.
        /// </summary>
        /// <param name="pacienteDto">O DTO do Paciente a ser criado.</param>
        /// <returns>A entidade Paciente criada.</returns>
        [HttpPost("new")]
        [SwaggerOperation(Summary = "Cria uma nova entidade Paciente", Description = "Cria uma nova entidade Paciente.")]
        [SwaggerResponse(201, "Paciente criado com sucesso")]
        [SwaggerResponse(500, "Entrada inválida ou erro interno")]
        public async Task<IActionResult> CreatePaciente([FromBody] PacienteDtos pacienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pacienteDto.Plano = await _planoRepository.GetById(pacienteDto.IdPlano);
            var newPaciente = await _pacienteRepository.Create(pacienteDto);
            newPaciente.NrCpf = Tools.StringTools.OnlyNumbers(newPaciente.NrCpf);
            newPaciente.NrTelefone = Tools.StringTools.OnlyNumbers(newPaciente.NrTelefone);

            return CreatedAtAction(nameof(GetPacienteById), new { id = newPaciente.IdPaciente }, newPaciente);
        }

        /// <summary>
        /// Atualiza uma entidade Paciente pelo ID.
        /// </summary>
        /// <param name="id">O ID do Paciente.</param>
        /// <param name="pacienteDto">Os dados do Paciente a ser atualizado. Preencher somente o que for necessário alterar</param>
        /// <returns>A entidade Paciente atualizada.</returns>
        [HttpPut("update/id/{id}")]
        [SwaggerOperation(Summary = "Atualiza uma entidade Paciente pelo ID", Description = "Atualiza uma entidade Paciente pelo seu ID. Preencher somente o que for necessário alterar")]
        [SwaggerResponse(200, "Paciente atualizado com sucesso")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> UpdatePacienteByID(int id, [FromBody] PacienteUpdateDtos pacienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPaciente = await _pacienteRepository.GetById(id);
            if (existingPaciente == null)
            {
                return NotFound("Paciente não encontrado");
            }

            var nmPaciente = string.IsNullOrEmpty(pacienteDto.NmPaciente) ? existingPaciente.NmPaciente : pacienteDto.NmPaciente;
            var nrCpf = string.IsNullOrEmpty(pacienteDto.NrCpf) ? existingPaciente.NrCpf : Tools.StringTools.OnlyNumbers(pacienteDto.NrCpf);
            var nrTelefone = string.IsNullOrEmpty(pacienteDto.NrTelefone) ? existingPaciente.NrTelefone : Tools.StringTools.OnlyNumbers(pacienteDto.NrTelefone);
            var dsEmail = string.IsNullOrEmpty(pacienteDto.DsEmail) ? existingPaciente.DsEmail : pacienteDto.DsEmail;
            var dtNascimento = (pacienteDto.DtNascimento == null) ? existingPaciente.DtNascimento : pacienteDto.DtNascimento;
            var dsSexo = string.IsNullOrEmpty(pacienteDto.DsSexo) ? existingPaciente.DsSexo : pacienteDto.DsSexo;
            var plano = new Models.Plano();
            var idPlano = existingPaciente.IdPlano;
            if (!string.IsNullOrEmpty(pacienteDto.DsCodigoPlano))
            {
                plano = await _planoRepository.GetByDsCodigoPlano(pacienteDto.DsCodigoPlano);
                if (plano == null)
                {
                    return NotFound("Plano não encontrado");
                }
                else
                {
                    idPlano = plano.IdPlano;
                }
            }
            

            var updatedPacienteDto = new PacienteDtos
            {
                NmPaciente = nmPaciente,
                NrCpf = nrCpf,
                NrTelefone = nrTelefone,
                DsEmail = dsEmail,
                DtNascimento = (DateTime)dtNascimento,
                DsSexo = dsSexo,
                IdPlano = idPlano,
                Plano = plano
            };

            var updatedPaciente = await _pacienteRepository.UpdateById(id, updatedPacienteDto);
            return Ok(updatedPaciente);
        }

        /// <summary>
        /// Atualiza uma entidade Paciente pelo CPF.
        /// </summary>
        /// <param name="cpf">O CPF do Paciente.</param>
        /// <param name="pacienteDto">Os dados do Paciente a ser atualizado. Preencher somente o que for necessário alterar</param>
        /// <returns>A entidade Paciente atualizada.</returns>
        [HttpPut("update/cpf/{cpf}")]
        [SwaggerOperation(Summary = "Atualiza uma entidade Paciente pelo CPF", Description = "Atualiza uma entidade Paciente pelo seu CPF. Preencher somente o que for necessário alterar")]
        [SwaggerResponse(200, "Paciente atualizado com sucesso")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> UpdatePacienteByCPF(string cpf, [FromBody] PacienteUpdateDtos pacienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cpf = Tools.StringTools.OnlyNumbers(cpf);
            var existingPaciente = await _pacienteRepository.GetByNrCpf(cpf);
            if (existingPaciente == null)
            {
                return NotFound("Paciente não encontrado");
            }

            var nmPaciente = string.IsNullOrEmpty(pacienteDto.NmPaciente) ? existingPaciente.NmPaciente : pacienteDto.NmPaciente;
            var nrCpf = string.IsNullOrEmpty(pacienteDto.NrCpf) ? existingPaciente.NrCpf : Tools.StringTools.OnlyNumbers(pacienteDto.NrCpf);
            var nrTelefone = string.IsNullOrEmpty(pacienteDto.NrTelefone) ? existingPaciente.NrTelefone : Tools.StringTools.OnlyNumbers(pacienteDto.NrTelefone);
            var dsEmail = string.IsNullOrEmpty(pacienteDto.DsEmail) ? existingPaciente.DsEmail : pacienteDto.DsEmail;
            var dtNascimento = (pacienteDto.DtNascimento == null) ? existingPaciente.DtNascimento : pacienteDto.DtNascimento;
            var dsSexo = string.IsNullOrEmpty(pacienteDto.DsSexo) ? existingPaciente.DsSexo : pacienteDto.DsSexo;
            var idPlano = pacienteDto.IdPlano == null ? existingPaciente.IdPlano : pacienteDto.IdPlano.Value;
            var plano = new Models.Plano();
            if (!string.IsNullOrEmpty(pacienteDto.DsCodigoPlano))
            {
                plano = await _planoRepository.GetByDsCodigoPlano(pacienteDto.DsCodigoPlano);
                if (plano == null)
                {
                    return NotFound("Plano não encontrado");
                }
                idPlano = plano.IdPlano;
            } 
            else
            {
                plano = await _planoRepository.GetById(idPlano);
            }


            var updatedPacienteDto = new PacienteDtos
            {
                NmPaciente = nmPaciente,
                NrCpf = nrCpf,
                NrTelefone = nrTelefone,
                DsEmail = dsEmail,
                DtNascimento = (DateTime)dtNascimento,
                DsSexo = dsSexo,
                IdPlano = idPlano,
                Plano = plano
            };

            var updatedPaciente = await _pacienteRepository.UpdateByCPF(cpf, updatedPacienteDto);            
            return Ok(updatedPaciente);
        }

        /// <summary>
        /// Deleta uma entidade Paciente pelo ID. Deleção simples.
        /// </summary>
        /// <param name="id">O ID do Paciente.</param>
        /// <returns>Confirmação de exclusão.</returns>
        [HttpDelete("delete/id/{id}")]
        [SwaggerOperation(Summary = "Deleta uma entidade Paciente pelo ID", Description = "Deleta uma entidade Paciente pelo seu ID.")]
        [SwaggerResponse(200, "Paciente deletado com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> DeletePacienteById(int id)
        {
            var result = await _pacienteRepository.DeleteById(id);
            if (!result)
            {
                return NotFound("Paciente não encontrado");
            }
            return Ok("Paciente deletado com sucesso");
        }

        /// <summary>
        /// Deleta uma entidade Paciente pelo CPF. Deleta com tratamento dos dados
        /// </summary>
        /// <param name="nrCpf">O CPF do Paciente.</param>
        /// <returns>Confirmação de exclusão.</returns>
        [HttpDelete("delete/cpf/{nrCpf}")]
        [SwaggerOperation(Summary = "Deleta uma entidade Paciente pelo CPF", Description = "Deleta uma entidade Paciente pelo seu CPF.")]
        [SwaggerResponse(200, "Paciente deletado com sucesso")]
        [SwaggerResponse(404, "Paciente não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> DeletePacienteByCpf(string nrCpf)
        {
            // Remove caracteres especiais do CPF
            var cpf = Tools.StringTools.OnlyNumbers(nrCpf);
            var result = await _pacienteRepository.DeleteByCPF(cpf);
            if (!result)
            {
                return NotFound("Paciente não encontrado");
            }
            return Ok("Paciente deletado com sucesso");
        }
    }
}
