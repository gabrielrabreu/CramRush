using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizationServiceCollectionExtensions
    {
        public static void AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new("en"),
                    new("pt-BR"),
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        public static void UseCustomLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()!.Value);
        }
    }
}
