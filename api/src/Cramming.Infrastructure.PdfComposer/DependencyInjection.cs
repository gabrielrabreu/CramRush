using Cramming.Application.Notecards.Interfaces;
using Cramming.Application.Practices.Interfaces;
using Cramming.Infrastructure.PdfComposer.Composers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPdfComposerServices(this IServiceCollection services)
        {
            services.AddScoped<INotecardComposer, NotecardComposer>();
            services.AddScoped<IPracticeComposer, PracticeComposer>();

            return services;
        }
    }
}
