using GloboClima.Dominio.Models.PaisResponse;

namespace GloboClima.Dominio.Interfaces
{
    public interface IServicoRestCountries
    {
        Task<PaisResponse?> ObterDadosPais(string codigo);
    }
}
