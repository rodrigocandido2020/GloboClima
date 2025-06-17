using GloboClima.Servico.ServicosAPI;

namespace GloboClima.API.ProgramStart
{
    public class ConfiguracaoDeInjecaoDeDependencia
    {
        public static void BindServices(IServiceCollection services)
        {
            services.AddHttpClient<ServicoOpenWeatherMap>();
            services.AddHttpClient<ServicoRestCountries>();
        }
    }
}
