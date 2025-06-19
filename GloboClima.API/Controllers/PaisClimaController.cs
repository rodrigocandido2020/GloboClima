using GloboClima.Servico.Servicos;
using GloboClima.Servico.ServicosAPI;
using Microsoft.AspNetCore.Mvc;

namespace GloboClima.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisClimaController : ControllerBase
    {
        private readonly ServicoPaisClima _servicoPaisClima;

        public PaisClimaController(
            ServicoPaisClima servicoPaisClima
            )
        {
            _servicoPaisClima = servicoPaisClima;
        }

        [HttpGet("clima")]
        public async Task<IActionResult> ObterClima([FromQuery] string? cidade)
        {
            var resultado = await _servicoPaisClima.ObterClimaCidade(cidade);
            return Ok(resultado);
        }

        [HttpGet("pais")]
        public async Task<IActionResult> ObterPais([FromQuery] string? codigo)
        {
            var resultado = await _servicoPaisClima.ObterDadosPais(codigo);
            return Ok(resultado);
        }
    }
}
