using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.Servicos;
using GloboClima.Servico.ServicosAPI;

namespace GloboClima.API.ProgramStart
{
    public class ConfiguracaoDeInjecaoDeDependencia
    {
        public static void BindServices(IServiceCollection services)
        {
            services.AddHttpClient<IServicoOpenWeatherMap, ServicoOpenWeatherMap>();
            services.AddHttpClient<IServicoRestCountries, ServicoRestCountries>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddSingleton<ServicoUsuario>();
            services.AddSingleton<CriarUsuarioAdmin>();
            services.AddSingleton<IServicoPaisClima, ServicoClimaPais>();
            services.AddScoped<IServicoFavorito, ServicoFavorito>();
        }
    }
}
