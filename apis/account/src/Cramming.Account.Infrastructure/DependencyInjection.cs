using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Infrastructure.Data;
using Cramming.Account.Infrastructure.Identity;
using Cramming.Account.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cramming.Account.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ApplicationDbContextInitialiser>();

            services
                .AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                        ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:SecretKey")!))
                    };
                });
            services.AddAuthorization();

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
