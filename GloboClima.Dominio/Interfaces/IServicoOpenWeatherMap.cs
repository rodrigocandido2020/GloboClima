using GloboClima.Dominio.Models.WeatherResponses;

namespace GloboClima.Dominio.Interfaces
{
    public interface IServicoOpenWeatherMap
    {
        Task<WeatherResponse?> ObterClimaPorCidade(string cidade);
    }
}
