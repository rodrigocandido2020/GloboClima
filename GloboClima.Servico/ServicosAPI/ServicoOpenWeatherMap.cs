using GloboClima.Dominio.Interfaces;
using GloboClima.Dominio.Models.WeatherResponses;
using System.Text.Json;

namespace GloboClima.Servico.ServicosAPI
{
    public class ServicoOpenWeatherMap: IServicoOpenWeatherMap
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "d55abc2c6de2871ed289dc7339e26849";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        public ServicoOpenWeatherMap(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse?> ObterClimaPorCidade(string cidade)
        {
            var url = $"{BaseUrl}?q={cidade}&appid={ApiKey}&units=metric&lang=pt";

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
