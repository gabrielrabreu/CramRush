namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddCustomLocalization();
            services.AddCustomExceptionHandler();
            services.AddCustomAuthentication();
            services.AddCustomHealthChecks();
            services.AddCustomSwagger();

            return services;
        }
    }
}
