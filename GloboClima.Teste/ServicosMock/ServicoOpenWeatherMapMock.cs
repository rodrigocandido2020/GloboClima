using GloboClima.Dominio.Interfaces;
using GloboClima.Dominio.Models.WeatherResponses;

namespace GloboClima.Teste.ServicosMock
{
    public class ServicoOpenWeatherMapMock : IServicoOpenWeatherMap
    {
        public Task<WeatherResponse?> ObterClimaPorCidade(string cidade)
        {
            return Task.FromResult(new WeatherResponse { });
        }
    }
}
