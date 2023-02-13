using System.Dynamic;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace FeiraLivre.Application.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IConfiguration configuration)
        {
            _next           = next;
            _logger         = logger;
            _configuration  = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Request");
                
                await _next(context);

                _logger.LogInformation("Response");
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message, error.StackTrace);

                dynamic objRetorno = new ExpandoObject();

                objRetorno.Code     = 0;
                objRetorno.Mensagem = error?.Message;
                objRetorno.Tipo     = error?.GetType().Name;

                var response = context.Response;

                response.ContentType = "application/json";

                switch (error)
                {
                    case KeyNotFoundException ex:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentException ex:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ConstraintException ex:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        objRetorno.InternalCode = Guid.NewGuid().ToString().ToUpper();
                        objRetorno.Message      = $"Erro ao processar a solicitação. Código do erro: {objRetorno.InternalCode}";

                        break;
                }

                objRetorno.Code = response.StatusCode;

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    AllowTrailingCommas = true
                };

                string resultado = JsonSerializer.Serialize(objRetorno, options);
                await response.WriteAsync(resultado);
            }
        }

    }
}