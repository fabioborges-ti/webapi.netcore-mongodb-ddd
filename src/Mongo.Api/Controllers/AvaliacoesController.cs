using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mongo.Domain.Dtos.Restaurante;
using Mongo.Domain.Schemas;
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
    public class AvaliacoesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRestauranteService _service;

        public AvaliacoesController(ILogger logger, IRestauranteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("restaurante/{id}/enviar")]
        public async Task<ActionResult> Avaliar(string id, [FromBody] AvaliacaoSchema request)
        {
            const string contextName = nameof(Avaliar);

            var validationRules = new AvaliacaoValidator();
            var validationResult = await validationRules.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var listErrors = new List<string>();

                var erros = validationResult.Errors.Select(p => p.ErrorMessage);

                listErrors.AddRange(erros);

                return BadRequest(new { erros = listErrors });
            }

            try
            {
                var result = await _service.Avaliar(id, request);

                return result ? Ok(new { data = "Restaurante avaliado com sucesso" }) : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }

        [HttpGet("restaurante/{id}")]
        public async Task<ActionResult<IEnumerable<RestauranteTop3>>> ObterAvaliacoes(string id)
        {
            const string contextName = nameof(ObterAvaliacoes);

            try
            {
                var result = await _service.ObterAvaliacoes(id);

                return result == null ? NoContent() : Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: { ex.Message } - { ex.StackTrace }, Context: { contextName }");

                return BadRequest();
            }
        }

        [HttpGet("top3")]
        public async Task<ActionResult<IEnumerable<RestauranteTop3>>> ObterTop3()
        {
            const string contextName = nameof(ObterTop3);

            try
            {
                var result = await _service.ObterTop3();

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
