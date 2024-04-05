using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Infrastructure.Data;
using Cramming.Account.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cramming.Account.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
