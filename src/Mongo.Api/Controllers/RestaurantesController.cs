using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mongo.Domain.Dtos.Restaurante;
using Mongo.Domain.Validations;
using Mongo.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRestauranteService _service;

        public RestaurantesController(ILogger logger, IRestauranteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Criar([FromBody] RestauranteDtoCreate request)
        {
            const string contextName = nameof(Criar);

            var validationRules = new RestauranteCreateValidator();
            var validationResult = await validationRules.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var listErrors = new List<string>();

                var erros = validationResult.Errors.Select(p => p.ErrorMessage);

                listErrors.AddRange(erros);

                return BadRequest(new
                {
                    erros = listErrors
                });
            }

            try
            {
                await _service.Criar(request);

                return Ok(new
                {
                    data = "Registro inserido com sucesso."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Editar(string id, [FromBody] RestauranteDtoUpdate request)
        {
            const string contextName = nameof(Editar);

            var validationRules = new RestauranteUpdateValidator();
            var validationResult = await validationRules.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var listErrors = new List<string>();

                var erros = validationResult.Errors.Select(p => p.ErrorMessage);

                listErrors.AddRange(erros);

                return BadRequest(new
                {
                    erros = listErrors
                });
            }

            if (string.IsNullOrEmpty(id)) return BadRequest(new { data = "Id inválido" });

            if (id != request.Id) return BadRequest(new { data = "Id inválido" });

            try
            {
                var result = await _service.Editar(id, request);

                return result 
                        ? Ok(new { data = "Registro atualizado com sucesso."}) 
                        : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(string id)
        {
            const string contextName = nameof(Remover);

            if (string.IsNullOrEmpty(id)) return BadRequest(new { data = "Id inválido" });

            try
            {
                var result = await _service.Remover(id);

                return result
                    ? Ok(new { data = "Registro removido com sucesso." })
                    : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Obter()
        {
            const string contextName = nameof(Obter);

            try
            {
                var result = await _service.Obter();

                return result == null ? NoContent() : Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} - {ex.StackTrace}, Context: {contextName}");

                return BadRequest();
            }
        }

        [HttpGet("por-id/{id}")]
        public async Task<ActionResult> ObterPorId(string id)
        {
            const string contextName = nameof(ObterPorId);

            try
            {
                var result = await _service.ObterPorId(id);

                return result == null ? NoContent() : Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }

        [HttpGet("por-nome/{nome}")]
        public async Task<ActionResult> ObterPorNome(string nome)
        {
            const string contextName = nameof(ObterPorNome);

            try
            {
                var result = await _service.ObterPorNome(nome);

                return result == null ? NoContent() : Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }
    }
}
