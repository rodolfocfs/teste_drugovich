using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace WebAPIGerenciadorDeClientes.Common.Middlewares.Localization
{
    public static class ApiLocalizationExtensions
    {
        public static IApplicationBuilder UsePtBrLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            return app;
        }
    }
}
