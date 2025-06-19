using GloboClima.Dominio.Models.PaisResponse;

namespace GloboClima.Dominio.Interfaces
{
    public interface IServicoRestCountries
    {
        Task<PaisResponse?> ObterPaisPorCodigo(string codigo);
    }
}
