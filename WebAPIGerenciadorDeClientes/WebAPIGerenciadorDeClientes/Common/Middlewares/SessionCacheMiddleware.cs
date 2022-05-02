using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace WebAPIGerenciadorDeClientes.Common.Middlewares.SessionCache
{
    public class SessionCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SessionCacheMiddleware> _logger;

        public SessionCacheMiddleware(RequestDelegate next,
            ILogger<SessionCacheMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                bool shouldAuthorize = context.GetEndpoint().Metadata.Select(x => x.ToString()).Any(attr => attr.Equals("Microsoft.AspNetCore.Authorization.AuthorizeAttribute"));
                bool hasAuthHeader = context.Request.Headers.TryGetValue("Authorization", out StringValues auth);
                if (shouldAuthorize && hasAuthHeader)
                {
                    string token = auth.ToString().Split(' ')[1];
                    ValidateSession(context, token);
                }
            }
            await _next(context);
        }

        private void ValidateSession(HttpContext context, string token)
        {
            var email = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (email == null)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
