using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.ServicosAPI;

namespace GloboClima.API.ProgramStart
{
    public class ConfiguracaoDeInjecaoDeDependencia
    {
        public static void BindServices(IServiceCollection services)
        {
            services.AddHttpClient<IServicoOpenWeatherMap, ServicoOpenWeatherMap>();
            services.AddHttpClient<IServicoRestCountries, ServicoRestCountries>();
        }
    }
}
