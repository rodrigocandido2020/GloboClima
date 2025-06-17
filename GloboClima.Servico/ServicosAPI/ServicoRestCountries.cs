using GloboClima.Dominio.Models.PaisResponse;
using System.Text.Json;

namespace GloboClima.Servico.ServicosAPI
{
    public class ServicoRestCountries
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://restcountries.com/v3.1/alpha/";
        public ServicoRestCountries(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaisResponse?> ObterPaisPorCodigo(string codigo)
        {
            var url = $"{BaseUrl}{codigo}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var lista = JsonSerializer.Deserialize<List<PaisResponse>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return lista?.FirstOrDefault();
            }

            return null;
        }
    }
}
