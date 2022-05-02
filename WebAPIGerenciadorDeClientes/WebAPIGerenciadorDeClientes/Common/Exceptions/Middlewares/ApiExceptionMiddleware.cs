using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Primitives;

namespace WebAPIGerenciadorDeClientes.Common.Exceptions.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next,
            ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiError error = _options.AddResponseDetails?.Invoke(exception)
                ?? ApiErrorFactory.New(exception);

            LogApiException(exception, error);

            return CreateResponse(context, error);
        }

        private static Task CreateResponse(HttpContext context, ApiError error)
        {
            if (error.Status == HttpStatusCode.Unauthorized)
                context.Response.Headers.Add(KeyValuePair.Create("www-authenticate", new StringValues("invalid_token")));
            var result = JsonSerializer.Serialize(error,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.Status;
            return context.Response.WriteAsync(result);
        }

        private void LogApiException(Exception exception, ApiError error)
        {
            var innerExMessage = GetInnermostExceptionMessage(exception);
            var level = _options.DetermineLogLevel?.Invoke(exception) ?? GetLogLevel(exception);
            //_logger.Log(level, exception, "ERROR: {message} -- {ErrorId}.", innerExMessage, error.Id);
            _logger.LogError($"ERROR: {innerExMessage} -- {error.Id}.");
        }

        private static LogLevel GetLogLevel(Exception exception) =>
            exception switch
            {
                InvalidInputException _ => LogLevel.Warning,
                _ => LogLevel.Error
            };

        private string GetInnermostExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnermostExceptionMessage(exception.InnerException) + " // " + exception.Message;

            return exception.Message + " // " + exception.StackTrace;
        }
    }
}
