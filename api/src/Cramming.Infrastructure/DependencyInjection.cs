﻿namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDataServices();
            services.AddDocumentComposerServices();

            return services;
        }
    }
}
