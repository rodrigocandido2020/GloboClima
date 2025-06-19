using GloboClima.Servico.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GloboClima.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritosController : ControllerBase
    {
        private readonly ServicoFavorito _servicoFavorito;
        
        public FavoritosController(ServicoFavorito servicoFavorito)
        {
            _servicoFavorito = servicoFavorito;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarFavorito([FromBody] string cidade)
        {
            var resultado = await _servicoFavorito.SalvarFavoritos(cidade);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ListarFavoritos()
        {
            var favoritos = await _servicoFavorito.ListarFavoritos();
            return Ok(favoritos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFavoritos(string id)
        {
            await _servicoFavorito.DeletarFavoritos(id);
            return NoContent();
        }

    }
}
