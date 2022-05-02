using Microsoft.AspNetCore.Mvc.Controllers;

namespace WebAPIGerenciadorDeClientes.Common.Middlewares
{
    public class EnvironmentMiddleware
    {
        private readonly ILogger<EnvironmentMiddleware> _logger;
        private readonly RequestDelegate _next;

        public EnvironmentMiddleware(RequestDelegate next, ILogger<EnvironmentMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            await _next(context);
        }

        private bool IsEnvironmentAllowed(Endpoint endpoint)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var attribute = GetAttribute(endpoint);
            return attribute == null || (!attribute.AllowedMode && !attribute.ForbiddenMode) || (attribute.AllowedMode && attribute.IsAllowed(environment) || attribute.ForbiddenMode && !attribute.IsForbidden(environment));
        }

        private EnvironmentAttribute GetAttribute(Endpoint endpoint)
        {
            var actionDescriptor = endpoint.Metadata.OfType<ControllerActionDescriptor>().SingleOrDefault();
            var envAttributes = actionDescriptor?.MethodInfo.GetCustomAttributes(typeof(EnvironmentAttribute), false).Cast<EnvironmentAttribute>();
            return envAttributes?.SingleOrDefault();
        }
    }
}
