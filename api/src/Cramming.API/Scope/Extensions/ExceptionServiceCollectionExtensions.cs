using Cramming.API.Scope.Middlewares;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExceptionServiceCollectionExtensions
    {
        public static void AddCustomExceptionHandler(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
