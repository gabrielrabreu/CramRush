using Cramming.Account.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class CustomWebApplicationFactory(BearerContext bearerContext, DatabaseContext databaseContext) : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                services
                    .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                    .AddDbContext<ApplicationDbContext>((sp, options) =>
                    {
                        options.UseSqlite(databaseContext.GetConnection());
                    });

                services.AddAuthentication("Test").AddJwtBearer("Test", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = bearerContext.Issuer,
                        ValidAudience = bearerContext.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerContext.Secret)),
                        ValidateIssuerSigningKey = true
                    };
                });
                services.AddAuthorization();
            });
        }
    }
}
