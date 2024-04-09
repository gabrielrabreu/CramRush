namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckServiceCollectionExtensions
    {
        public static void AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        public static void UseCustomHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");
        }
    }
}
