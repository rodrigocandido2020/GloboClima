using GloboClima.Dominio.Models.PaisResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GloboClima.Servico.Servicos
{
    public class ServicoRestCountries
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://restcountries.com/v3.1/alpha/";
        public ServicoRestCountries(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaisResponse?> ObterPaisPorCodigoAsync(string codigo)
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
