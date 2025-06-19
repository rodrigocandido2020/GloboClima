using GloboClima.Dominio.Models.PaisResponse;
using GloboClima.Dominio.Models.WeatherResponses;

namespace GloboClima.Dominio.Interfaces
{
    public interface IServicoPaisClima
    {
        Task<WeatherResponse> ObterClimaCidade(string? cidade);
        Task<PaisResponse> ObterDadosPais(string? codigo);
    }
}
