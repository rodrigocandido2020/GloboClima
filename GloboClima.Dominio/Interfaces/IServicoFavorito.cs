using GloboClima.Dominio.Models.Favoritos;

namespace GloboClima.Dominio.Interfaces
{
    public interface IServicoFavorito
    {
        Task<Favorito> SalvarFavoritos(string? cidade);
        Task VerificarCidadeExistente(string cidade);
        Task<List<Favorito>> ListarFavoritos();
        Task DeletarFavoritos(string id);
    }
}
