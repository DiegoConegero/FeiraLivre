using FeiraLivre.Core.Constants;
using FeiraLivre.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

//using Microsoft.Extensions.
using System.Diagnostics;
using System.Dynamic;
using System.Net;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace FeiraLivre.Application.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        //private LogConfig _logConfig;
        private readonly string erro = "Erro";
        private readonly string trace = "Trace";

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            
            try
            {
                var guid = string.Empty;
                var responseContent = string.Empty;

                //_logConfig = new LogConfig();
                //_configuration.Bind($"{nameof(LogConfig)}{trace}", _logConfig);

                Stream originalBody = context.Response.Body;

                var memoryStream = new MemoryStream();
                //guid = LogManager.IncluirLog(context, TipoAcaoEnum.Request, LogEnum.Trace, responseContent, _logConfig).Result;

                await _next(context);

                context.Response.Body = memoryStream;
                memoryStream.Position = 0;
                responseContent = new StreamReader(memoryStream).ReadToEnd();
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);

                //guid = LogManager.IncluirLog(context, TipoAcaoEnum.Response, LogEnum.Trace, responseContent, _logConfig).Result;

                memoryStream.Close();

            }
            catch (Exception error)
            {
                //_configuration.Bind($"{nameof(LogConfig)}{erro}", _logConfig);

                dynamic objRetorno = new ExpandoObject();

                objRetorno.Code = 0;
                objRetorno.Mensagem = error?.Message;
                objRetorno.Tipo = error?.GetType().Name;

                var response = context.Response;

                //response.ContentType = TipoConteudoConstant.Json;

                switch (error)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var stackTrace = new StackTrace(error);
                        var assembly = Assembly.GetExecutingAssembly();
                        var metodo = stackTrace.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == assembly);

                        //objRetorno.InternalCode = await LogManager.IncluirLog(context, TipoAcaoEnum.Exception, LogEnum.Error, string.Empty, _logConfig, error);
                        objRetorno.Message = $"Erro ao processar a solicitação. Código do erro: {objRetorno.InternalCode}";

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