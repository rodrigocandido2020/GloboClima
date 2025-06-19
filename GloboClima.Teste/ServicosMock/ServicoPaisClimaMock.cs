using GloboClima.Dominio.Interfaces;
using GloboClima.Dominio.Models.PaisResponse;
using GloboClima.Dominio.Models.WeatherResponses;

namespace GloboClima.Teste.ServicosMock
{
    public class ServicoPaisClimaMock : IServicoRestCountries
    {
        public Task<PaisResponse?> ObterPaisPorCodigo(string codigo)
        {
            return Task.FromResult(new PaisResponse{ });
        }
    }
}
