using GloboClima.Dominio.Models.Favoritos;
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
        public async Task<IActionResult> AdicionarCidadeFavorita([FromBody] Favorito favorita)
        {
            if (string.IsNullOrWhiteSpace(favorita.Codigo) || string.IsNullOrWhiteSpace(favorita.Nome))
            {
                return BadRequest("Código e nome são obrigatórios.");
            }

            await _servicoFavorito.SalvarCidadeFavoritaAsync(favorita);
            return Ok("Cidade salva como favorita.");
        }

        [HttpGet]
        public async Task<IActionResult> ListarCidadesFavoritas()
        {
            var favoritos = await _servicoFavorito.ListarFavorita();
            return Ok(favoritos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCidadeFavorita(string id)
        {
            var sucesso = await _servicoFavorito.DeletarFavorita(id);
            if (!sucesso)
                return NotFound("Cidade favorita não encontrada.");

            return Ok("Cidade favorita removida com sucesso.");
        }

    }
}
