using Amazon.DynamoDBv2.DataModel;
using GloboClima.Dominio.Interfaces;
using GloboClima.Servico.Servicos;
using GloboClima.Servico.ServicosAPI;

namespace GloboClima.API.Extensoes
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // APIs externas
            services.AddHttpClient<IServicoOpenWeatherMap, ServicoOpenWeatherMap>();
            services.AddHttpClient<IServicoRestCountries, ServicoRestCountries>();

            // AWS
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            // Serviços de domínio
            services.AddSingleton<IServicoPaisClima, ServicoClimaPais>();
            services.AddScoped<IServicoFavorito, ServicoFavorito>();
            services.AddSingleton<IServicoUsuario, ServicoUsuario>();
            services.AddSingleton<CriarUsuarioAdmin>();

            return services;
        }
    }
}
