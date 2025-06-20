using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GloboClima.API
{
    public static class ConfigurarStatusCodePages
    {
        public static void ConfigurarStatusCodesPage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                var requestPath = context.HttpContext.Request.Path;

                ProblemDetails detalhes = null;

                if (response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    detalhes = new ProblemDetails
                    {
                        Title = "Acesso Não Autorizado!",
                        Status = StatusCodes.Status401Unauthorized,
                        Detail = "Você não tem permissão para acessar este recurso. Token ausente ou inválido.",
                        Instance = requestPath
                    };
                }

                if (detalhes != null)
                {
                    response.ContentType = "application/problem+json";
                    var json = JsonConvert.SerializeObject(detalhes);
                    await response.WriteAsync(json);
                }
            });
        }

    }
}
