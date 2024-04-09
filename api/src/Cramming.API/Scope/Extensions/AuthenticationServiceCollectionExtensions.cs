using Cramming.API.Scope.Services;
using Cramming.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer").AddJwtBearer();
            services.AddAuthorization();

            services.AddHttpContextAccessor();

            services.AddScoped<IHttpSession, HttpSession>();
        }
    }
}
