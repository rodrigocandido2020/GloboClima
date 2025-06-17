using GloboClima.Servico.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace GloboClima.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisClimaController : ControllerBase
    {
        private readonly ServicoOpenWeatherMap _servicoOpenWeatherMap;
        private readonly ServicoRestCountries _servicoRestCountries;

        public PaisClimaController(
            ServicoOpenWeatherMap servicoOpenWeatherMap,
            ServicoRestCountries servicoRestCountries
            )
        {
            _servicoOpenWeatherMap = servicoOpenWeatherMap;
            _servicoRestCountries = servicoRestCountries;
        }

        [HttpGet("clima")]
        public async Task<IActionResult> ObterClima([FromQuery] string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                return BadRequest("Cidade é obrigatória.");

            var resultado = await _servicoOpenWeatherMap.ObterClimaPorCidadeAsync(cidade);

            if (resultado == null)
                return NotFound("Não foi possível obter o clima para a cidade informada.");

            return Ok(resultado);
        }

        [HttpGet("pais")]
        public async Task<IActionResult> ObterPais([FromQuery] string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return BadRequest("Código do país é obrigatório.");

            var pais = await _servicoRestCountries.ObterPaisPorCodigoAsync(codigo);

            if (pais == null)
                return NotFound("País não encontrado para o código informado.");

            return Ok(pais);
        }
    }
}
