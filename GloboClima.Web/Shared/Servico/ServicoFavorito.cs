using GloboClima.Web.Shared.ViewModels;

namespace GloboClima.Web.Shared.Servico
{
    public class ServicoFavorito
    {
        private readonly ServicoAPI _api;

        public ServicoFavorito(ServicoAPI api)
        {
            _api = api;
        }

        public async Task<(List<FavoritoViewModel>? favoritos, string? erro)> ObterFavoritosAsync()
        {
            var (resultado, problema) = await _api.GetAsync<List<FavoritoViewModel>>("api/favoritos");

            if (problema != null)
            {
                return (null, problema.Detail);
            }

            return (resultado, null);
        }

        public async Task<(bool sucesso, List<FavoritoViewModel>? favoritosAtualizados, string? erro)> DeletarFavoritoAsync(string id)
        {
            var (sucesso, erroDelete) = await _api.DeleteAsync($"api/favoritos/{id}");

            if (sucesso)
            {
                var (resultado, problema) = await _api.GetAsync<List<FavoritoViewModel>>("api/favoritos");
                return (true, resultado ?? new List<FavoritoViewModel>(), null);
            }
            else
            {
                return (false, null, "Erro ao deletar o favorito.");
            }
        }
    }
}
