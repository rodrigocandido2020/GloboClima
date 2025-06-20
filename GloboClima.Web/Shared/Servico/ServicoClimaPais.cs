using GloboClima.Web.Shared.ViewModels;

namespace GloboClima.Web.Shared.Servico
{
    public class ServicoClimaPais
    {
        private readonly ServicoAPI _api;

        public ServicoClimaPais(ServicoAPI api)
        {
            _api = api;
        }

        public async Task<(WeatherResponseViewModel? clima, string? erro)> ObterClimaAsync(string cidade)
        {
            var (clima, erroClima) = await _api.GetAsync<WeatherResponseViewModel>(
                $"api/ClimaPais/clima?cidade={Uri.EscapeDataString(cidade)}");

            if (erroClima != null)
                return (null, erroClima.Detail);

            return (clima, null);
        }

        public async Task<(PaisViewModel? pais, string? erro)> ObterPaisAsync(string codigoPais)
        {
            var (pais, erroPais) = await _api.GetAsync<PaisViewModel>($"api/ClimaPais/pais?codigo={codigoPais}");

            if (erroPais != null)
                return (null, erroPais.Detail);

            return (pais, null);
        }

        public async Task<string?> SalvarFavoritoAsync(string cidade)
        {
            var (_, problema) = await _api.PostAsync<FavoritoViewModel>("api/favoritos", cidade);

            return problema?.Detail;
        }
    }
}
