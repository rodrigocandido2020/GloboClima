using GloboClima.Dominio.Interfaces;
using GloboClima.Dominio.Models.WeatherResponses;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GloboClima.Servico.ServicosAPI
{
    public class ServicoOpenWeatherMap: IServicoOpenWeatherMap
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        public ServicoOpenWeatherMap(HttpClient httpClient, IOptions<OpenWeatherMapSettings> settings)
        {
            _httpClient = httpClient;
            _apiKey = settings.Value.ApiKey;
            _baseUrl = settings.Value.BaseUrl;
        }

        public async Task<WeatherResponse?> ObterClimaPorCidade(string cidade)
        {
            var url = $"{_baseUrl}?q={cidade}&appid={_apiKey}&units=metric&lang=pt";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null;
        }
    }
}
