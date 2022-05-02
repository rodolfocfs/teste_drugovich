
namespace WebAPIGerenciadorDeClientes.Common.Middlewares
{
    public static class EnvironmentExtensions
    {
        public static IApplicationBuilder UseEnvironmentMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EnvironmentMiddleware>();
        }
    }
}
