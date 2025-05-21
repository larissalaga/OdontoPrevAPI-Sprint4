using Microsoft.AspNetCore.Mvc;
using OdontoPrevAPI.Dtos;
using OdontoPrevAPI.Repositories.Interfaces;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoPrevAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar entidades Plano.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoRepository _planoRepository;

        /// <summary>
        /// Inicializa uma nova inst�ncia da classe <see cref="PlanoController"/>.
        /// </summary>
        /// <param name="planoRepository">O reposit�rio para entidades Plano.</param>
        public PlanoController(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }

        /// <summary>
        /// Obt�m todas as entidades Plano.
        /// </summary>
        /// <returns>Uma lista de entidades Plano.</returns>
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Obt�m todas as entidades Plano", Description = "Retorna uma lista de todas as entidades Plano.")]
        [SwaggerResponse(200, "Retorna a lista de entidades Plano")]
        [SwaggerResponse(500, "Nenhum plano encontrado")]
        public async Task<IActionResult> GetAllPlanos()
        {
            var planos = await _planoRepository.GetAll();
            return Ok(planos);
        }

        /// <summary>
        /// Obt�m uma entidade Plano pelo c�digo.
        /// </summary>
        /// <param name="dsCodigoPlano">O c�digo do Plano.</param>
        /// <returns>A entidade Plano.</returns>
        [HttpGet("codigo/{dsCodigoPlano}")]
        [SwaggerOperation(Summary = "Obt�m uma entidade Plano pelo c�digo", Description = "Retorna uma entidade Plano pelo seu c�digo.")]
        [SwaggerResponse(200, "Retorna a entidade Plano")]
        [SwaggerResponse(500, "Plano n�o encontrado")]
        public async Task<IActionResult> GetPlanoByDsCodigoPlano(string dsCodigoPlano)
        {
            var plano = await _planoRepository.GetByDsCodigoPlano(dsCodigoPlano);
            return Ok(plano);
        }

        /// <summary>
        /// Obt�m uma entidade Plano pelo ID.
        /// </summary>
        /// <param name="id">O ID do Plano.</param>
        /// <returns>A entidade Plano.</returns>
        [HttpGet("id/{id}")]
        [SwaggerOperation(Summary = "Obt�m uma entidade Plano pelo ID", Description = "Retorna uma entidade Plano pelo seu ID.")]
        [SwaggerResponse(200, "Retorna a entidade Plano")]
        [SwaggerResponse(500, "Plano n�o encontrado")]
        public async Task<IActionResult> GetPlanoById(int id)
        {
            var plano = await _planoRepository.GetById(id);
            return Ok(plano);
        }

        /// <summary>
        /// Cria uma nova entidade Plano.
        /// </summary>
        /// <param name="planoDto">O DTO do Plano a ser criado.</param>
        /// <returns>A entidade Plano criada.</returns>
        [HttpPost("new")]
        [SwaggerOperation(Summary = "Cria uma nova entidade Plano", Description = "Cria uma nova entidade Plano.")]
        [SwaggerResponse(201, "Plano criado com sucesso")]
        [SwaggerResponse(500, "Entrada inv�lida ou erro interno")]
        public async Task<IActionResult> CreatePlano([FromBody] PlanoDtos planoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPlano = await _planoRepository.Create(planoDto);
            return CreatedAtAction(nameof(GetPlanoById), new { id = newPlano.IdPlano }, newPlano);
        }

        /// <summary>
        /// Atualiza uma entidade Plano pelo ID.
        /// </summary>
        /// <param name="id">O ID do Plano.</param>
        /// <param name="planoDto">O DTO do Plano a ser atualizado.</param>
        /// <returns>A entidade Plano atualizada.</returns>
        [HttpPut("update/id/{id}")]
        [SwaggerOperation(Summary = "Atualiza uma entidade Plano pelo ID", Description = "Atualiza uma entidade Plano pelo seu ID usando Entity Framework.")]
        [SwaggerResponse(200, "Plano atualizado com sucesso")]
        [SwaggerResponse(404, "Plano n�o encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> UpdatePlanoById(int id, [FromBody] PlanoUpdateDtos planoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPlano = await _planoRepository.GetById(id);
            if (existingPlano == null)
            {
                return NotFound("Plano n�o encontrado");
            }

            var dsCodigoPlano = string.IsNullOrEmpty(planoDto.DsCodigoPlano) ? existingPlano.DsCodigoPlano : planoDto.DsCodigoPlano;
            var nmPlano = string.IsNullOrEmpty(planoDto.NmPlano) ? existingPlano.NmPlano : planoDto.NmPlano;

            var updatedPlanoDto = new PlanoDtos
            {
                DsCodigoPlano = dsCodigoPlano,
                NmPlano = nmPlano
            };

            var updatedPlano = await _planoRepository.Update(id, updatedPlanoDto);
            return Ok(updatedPlano);
        }

        /// <summary>
        /// Atualiza uma entidade Plano pelo c�digo.
        /// </summary>
        /// <param name="dsCodigoPlano">O c�digo do Plano.</param>
        /// <param name="planoDto">O DTO do Plano a ser atualizado.</param>
        /// <returns>A entidade Plano atualizada.</returns>
        [HttpPut("update/codigo/{dsCodigoPlano}")]
        [SwaggerOperation(Summary = "Atualiza uma entidade Plano pelo c�digo", Description = "Atualiza uma entidade Plano pelo seu c�digo usando o procedimento UPDATE_PLANO do Oracle.")]
        [SwaggerResponse(200, "Plano atualizado com sucesso")]
        [SwaggerResponse(404, "Plano n�o encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> UpdatePlanoByCodigo(string dsCodigoPlano, [FromBody] PlanoUpdateDtos planoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPlano = await _planoRepository.GetByDsCodigoPlano(dsCodigoPlano);
            if (existingPlano == null)
            {
                return NotFound("Plano n�o encontrado");
            }

            var nmPlano = string.IsNullOrEmpty(planoDto.NmPlano) ? existingPlano.NmPlano : planoDto.NmPlano;

            var updatedPlan = await _planoRepository.UpdateByCodigo(dsCodigoPlano, nmPlano);
            return Ok(updatedPlan);
        }

        /// <summary>
        /// Deleta uma entidade Plano pelo ID.
        /// </summary>
        /// <param name="id">O ID do Plano.</param>
        /// <returns>Confirma��o de exclus�o.</returns>
        [HttpDelete("delete/id/{id}")]
        [SwaggerOperation(Summary = "Deleta uma entidade Plano pelo ID", Description = "Deleta uma entidade Plano pelo seu ID utilizando Entity Framework.")]
        [SwaggerResponse(200, "Plano deletado com sucesso")]
        [SwaggerResponse(404, "Plano n�o encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> DeletePlanoById(int id)
        {
            var result = await _planoRepository.DeleteById(id);
            if (!result)
            {
                return NotFound("Plano n�o encontrado");
            }
            return Ok("Plano deletado com sucesso");
        }

        /// <summary>
        /// Deleta uma entidade Plano pelo c�digo.
        /// </summary>
        /// <param name="dsCodigoPlano">O c�digo do Plano. Deleta com tratamento dos dados</param>
        /// <returns>Confirma��o de exclus�o.</returns>
        [HttpDelete("delete/codigo/{dsCodigoPlano}")]
        [SwaggerOperation(Summary = "Deleta uma entidade Plano pelo c�digo", Description = "Deleta uma entidade Plano pelo seu c�digo utilizando o procedimento DELETE_PLANO do Oracle.")]
        [SwaggerResponse(200, "Plano deletado com sucesso")]
        [SwaggerResponse(404, "Plano n�o encontrado")]
        [SwaggerResponse(500, "Erro interno")]
        public async Task<IActionResult> DeletePlanoByCodigoPlano(string dsCodigoPlano)
        {
            var result = await _planoRepository.DeleteByCodigoPlano(dsCodigoPlano);
            if (!result)
            {
                return NotFound("Plano n�o encontrado");
            }
            return Ok("Plano deletado com sucesso");
        }
    }
}

