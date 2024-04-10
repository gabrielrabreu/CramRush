using Cramming.Application.Notecards.Interfaces;
using Cramming.Application.Practices.Interfaces;
using Cramming.Infrastructure.DocumentComposer.Composers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDocumentComposerServices(this IServiceCollection services)
        {
            services.AddScoped<INotecardComposer, NotecardComposer>();
            services.AddScoped<IPracticeComposer, PracticeComposer>();

            return services;
        }
    }
}
