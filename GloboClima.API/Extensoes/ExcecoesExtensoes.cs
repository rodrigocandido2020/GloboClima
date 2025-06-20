using System.Diagnostics;
using GloboClima.Dominio.Excecoes;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GloboClima.API.Extensoes
{
    public static class ExcecoesExtensoes
    {
        public static void ConfigurarManipuladorDeExcecoes(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");

                        var detalhesDoProblema = ConstruirDetalhesDoProblema(context, exception, logger);

                        context.Response.StatusCode = detalhesDoProblema.Status ?? StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/problem+json";

                        var json = JsonConvert.SerializeObject(detalhesDoProblema);
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }

        private static ProblemDetails ConstruirDetalhesDoProblema(HttpContext context, Exception exception, ILogger logger)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    ConfigurarNotFoundDetalhes(problemDetails, notFoundException);
                    break;

                case UnauthorizedException unauthorizedException:
                    ConfigurarUnauthorizedDetalhes(problemDetails, unauthorizedException);
                    break;

                case BadRequestException badRequestException:
                    ConfigurarBadRequestDetalhes(problemDetails, badRequestException);
                    break;

                case ConflictException conflictException:
                    ConfigurarconflictDetalhes(problemDetails, conflictException);
                    break;

                default:
                    ConfigurarDetalhesErroGenerico(problemDetails, exception, logger);
                    break;
            }

            return problemDetails;
        }

        private static void ConfigurarNotFoundDetalhes(ProblemDetails problemDetails, NotFoundException notFoundException)
        {
            problemDetails.Title = "Recurso Não Encontrado!";
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Detail = notFoundException.Message;
            problemDetails.Extensions["Details"] = notFoundException.StackTrace;
        }

        private static void ConfigurarUnauthorizedDetalhes(ProblemDetails problemDetails, UnauthorizedException unauthorizedException)
        {
            problemDetails.Title = "Acesso Não Autorizado!";
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Detail = unauthorizedException.Message;
            problemDetails.Extensions["Details"] = unauthorizedException.StackTrace;
        }

        private static void ConfigurarBadRequestDetalhes(ProblemDetails problemDetails, BadRequestException badRequestException)
        {
            problemDetails.Title = "Requisição Inválida!";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = badRequestException.Message;
            problemDetails.Extensions["Details"] = badRequestException.StackTrace;
        }
        private static void ConfigurarconflictDetalhes(ProblemDetails problemDetails, ConflictException conflictException)
        {
            problemDetails.Title = "Conflito de Requisição!";
            problemDetails.Status = StatusCodes.Status409Conflict;
            problemDetails.Detail = conflictException.Message;
            problemDetails.Extensions["Details"] = conflictException.StackTrace;
        }

        private static void ConfigurarDetalhesErroGenerico(ProblemDetails problemDetails, Exception exception, ILogger logger)
        {
            logger.LogError($"Unexpected error: {exception}");

            problemDetails.Title = "Erro Interno no Servidor";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Detail = exception.Demystify().ToString();
            problemDetails.Extensions["UnexpectedError"] = exception.Message;
        }
    }
}
