using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using OdontoPrevAPI.Repositories.Implementations;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar entidades Dentista.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DentistaController : ControllerBase
    {
        private readonly IDentistaRepository _dentistaRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DentistaController"/>.
        /// </summary>
        /// <param name="dentistaRepository">O repositório para entidades Dentista.</param>
        public DentistaController(IDentistaRepository dentistaRepository)
        {
            _dentistaRepository = dentistaRepository;
        }

        /// <summary>
        /// Obtém todas as entidades Dentista.
        /// </summary>
        /// <returns>Uma lista de entidades Dentista.</returns>
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Obtém todas as entidades Dentista", Description = "Retorna uma lista de todas as entidades Dentista.")]
        [SwaggerResponse(200, "Retorna a lista de entidades Dentista")]
        [SwaggerResponse(500, "Nenhum dentista encontrado")]
        public async Task<IActionResult> GetAllDentistas()
        {
            var dentistas = await _dentistaRepository.GetAll();
            return Ok(dentistas);
        }

        /// <summary>
        /// Obtém uma entidade Dentista pelo CRO.
        /// </summary>
        /// <param name="dsCro">O CRO do Dentista.</param>
        /// <returns>A entidade Dentista.</returns>
        [HttpGet("cro/{dsCro}")]
        [SwaggerOperation(Summary = "Obtém uma entidade Dentista pelo CRO", Description = "Retorna uma entidade Dentista pelo seu CRO.")]
        [SwaggerResponse(200, "Retorna a entidade Dentista")]
        [SwaggerResponse(500, "Dentista não encontrado")]
        public async Task<IActionResult> GetDentistaByCro(string dsCro)
        {
            var dentista = await _dentistaRepository.GetByDsCro(dsCro);
            return Ok(dentista);
        }

        /// <summary>
        /// Obtém uma entidade Dentista pelo ID.
        /// </summary>
        /// <param name="id">O ID do Dentista.</param>
        /// <returns>A entidade Dentista.</returns>
        [HttpGet("id/{id}")]
        [SwaggerOperation(Summary = "Obtém uma entidade Dentista pelo ID", Description = "Retorna uma entidade Dentista pelo seu ID.")]
        [SwaggerResponse(200, "Retorna a entidade Dentista")]
        [SwaggerResponse(500, "Dentista não encontrado")]
        public async Task<IActionResult> GetDentistaById(int id)
        {
            var dentista = await _dentistaRepository.GetById(id);
            return Ok(dentista);
        }

        /// <summary>
        /// Cria uma nova entidade Dentista.
        /// </summary>
        /// <param name="dentistaDto">O DTO do Dentista a ser criado.</param>
        /// <returns>A entidade Dentista criada.</returns>
        [HttpPost("new")]
        [SwaggerOperation(Summary = "Cria uma nova entidade Dentista", Description = "Cria uma nova entidade Dentista.")]
        [SwaggerResponse(201, "Dentista criado com sucesso")]
        [SwaggerResponse(500, "Entrada inválida ou erro interno")]
        public async Task<IActionResult> CreateDentista([FromBody] DentistaDtos dentistaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dentistaDto.NrTelefone = Tools.StringTools.OnlyNumbers(dentistaDto.NrTelefone);
            dentistaDto.DsDocIdentificacao = Tools.StringTools.OnlyNumbers(dentistaDto.DsDocIdentificacao);

            var newDentista = await _dentistaRepository.Create(dentistaDto);
            return CreatedAtAction(nameof(GetDentistaById), new { id = newDentista.IdDentista }, newDentista);
        }

        /// <summary>
        /// Atualiza uma entidade Dentista pelo ID.
        /// </summary>
        /// <param name="id">O ID do Dentista.</param>
        /// <param name="dentistaDto">O DTO do Dentista a ser atualizado.</param>
        /// <returns>A entidade Dentista atualizada.</returns>
        [HttpPut("update/id/{id}")]
        [SwaggerOperation(Summary = "Atualiza uma entidade Dentista pelo ID", Description = "Atualiza uma entidade Dentista pelo seu ID utilizando Entity Framework.")]
        [SwaggerResponse(200, "Dentista atualizado com sucesso")]
        [SwaggerResponse(404, "Dentista não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> UpdateDentistaById(int id, [FromBody] DentistaUpdateDtos dentistaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDentista = await _dentistaRepository.GetById(id);
            if (existingDentista == null)
            {
                return NotFound("Dentista não encontrado");
            }

            var nmDentista = string.IsNullOrEmpty(dentistaDto.NmDentista) ? existingDentista.NmDentista : dentistaDto.NmDentista;
            var dscCro = string.IsNullOrEmpty(dentistaDto.DsCro) ? existingDentista.DsCro : dentistaDto.DsCro;
            var nrTelefone = string.IsNullOrEmpty(dentistaDto.NrTelefone) ? existingDentista.NrTelefone : Tools.StringTools.OnlyNumbers(dentistaDto.NrTelefone);
            var dsEmail = string.IsNullOrEmpty(dentistaDto.DsEmail) ? existingDentista.DsEmail : dentistaDto.DsEmail;
            var dsDocIdentificacao = string.IsNullOrEmpty(dentistaDto.DsDocIdentificacao) ? existingDentista.DsDocIdentificacao : Tools.StringTools.OnlyNumbers(dentistaDto.DsDocIdentificacao);

            var updatedDentistaDto = new DentistaDtos
            {
                NmDentista = nmDentista,
                DsCro = dscCro,
                NrTelefone = nrTelefone,
                DsEmail = dsEmail,
                DsDocIdentificacao = dsDocIdentificacao
            };

            var updatedDentista = await _dentistaRepository.UpdateById(id, updatedDentistaDto);
            return Ok(updatedDentista);
        }

        /// <summary>
        /// Atualiza uma entidade Dentista pelo CRO.
        /// </summary>
        /// <param name="dsCro">O CRO do Dentista.</param>
        /// <param name="dentistaDto">O DTO do Dentista a ser atualizado.</param>
        /// <returns>A entidade Dentista atualizada.</returns>
        [HttpPut("update/cro/{dsCro}")]
        [SwaggerOperation(Summary = "Atualiza uma entidade Dentista pelo CRO", Description = "Atualiza uma entidade Dentista pelo seu CRO.")]
        [SwaggerResponse(200, "Dentista atualizado com sucesso")]
        [SwaggerResponse(404, "Dentista não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> UpdateDentistaByCro(string dsCro, [FromBody] DentistaUpdateDtos dentistaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDentista = await _dentistaRepository.GetByDsCro(dsCro);
            if (existingDentista == null)
            {
                return NotFound("Dentista não encontrado");
            }

            var nmDentista = string.IsNullOrEmpty(dentistaDto.NmDentista) ? existingDentista.NmDentista : dentistaDto.NmDentista;
            var dscCro = string.IsNullOrEmpty(dentistaDto.DsCro) ? existingDentista.DsCro : dentistaDto.DsCro;
            var nrTelefone = string.IsNullOrEmpty(dentistaDto.NrTelefone) ? existingDentista.NrTelefone : Tools.StringTools.OnlyNumbers(dentistaDto.NrTelefone);
            var dsEmail = string.IsNullOrEmpty(dentistaDto.DsEmail) ? existingDentista.DsEmail : dentistaDto.DsEmail;
            var dsDocIdentificacao = string.IsNullOrEmpty(dentistaDto.DsDocIdentificacao) ? existingDentista.DsDocIdentificacao : Tools.StringTools.OnlyNumbers(dentistaDto.DsDocIdentificacao);

            var updatedDentistaDto = new DentistaDtos
            {
                NmDentista = nmDentista,
                DsCro = dscCro,
                NrTelefone = nrTelefone,
                DsEmail = dsEmail,
                DsDocIdentificacao = dsDocIdentificacao
            };

            var updatedDentista = await _dentistaRepository.UpdateByCRO(dsCro, updatedDentistaDto);
            return Ok(updatedDentista);
        }

        /// <summary>
        /// Deleta uma entidade Dentista pelo ID.
        /// </summary>
        /// <param name="id">O ID do Dentista.</param>
        /// <returns>Confirmação de exclusão.</returns>
        [HttpDelete("delete/id/{id}")]
        [SwaggerOperation(Summary = "Deleta uma entidade Dentista pelo ID", Description = "Deleta uma entidade Dentista pelo seu ID utilizando Entity Framework.")]
        [SwaggerResponse(200, "Dentista deletado com sucesso")]
        [SwaggerResponse(404, "Dentista não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> DeleteDentistaById(int id)
        {
            var result = await _dentistaRepository.DeleteById(id);
            if (!result)
            {
                return NotFound("Dentista não encontrado");
            }
            return Ok("Dentista deletado com sucesso");
        }

        /// <summary>
        /// Deleta uma entidade Dentista pelo CRO.
        /// </summary>
        /// <param name="dsCro">O CRO do Dentista.</param>
        /// <returns>Confirmação de exclusão.</returns>
        [HttpDelete("delete/cro/{dsCro}")]
        [SwaggerOperation(Summary = "Deleta uma entidade Dentista pelo CRO", Description = "Deleta uma entidade Dentista pelo seu CRO.")]
        [SwaggerResponse(200, "Dentista deletado com sucesso")]
        [SwaggerResponse(404, "Dentista não encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> DeleteDentistaByCro(string dsCro)
        {
            // Remove caracteres especiais do CPF            
            var result = await _dentistaRepository.DeleteByCRO(dsCro);
            if (!result)
            {
                return NotFound("Dentista não encontrado");
            }
            return Ok("Dentista deletado com sucesso");
        }
    }
}
