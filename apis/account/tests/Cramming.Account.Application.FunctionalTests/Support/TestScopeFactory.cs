using Cramming.Account.Application.FunctionalTests.Support.Data;
using Cramming.Account.Infrastructure;
using Cramming.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Cramming.Account.Application.FunctionalTests.Support
{
    public static class TestScopeFactory
    {
        public static IServiceScopeFactory Create(ITestDatabase database)
        {
            var services = new ServiceCollection();
            var configuration = CreateConfiguration();

            services.AddLocalization();

            services.AddLogging(builder => builder.AddConsole());

            services.AddApplicationServices();

            services.AddSingleton(configuration);

            services.AddInfrastructureServices(configuration);

            services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.UseSqlite(database.GetConnection());
                });

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        public static IConfiguration CreateConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();

            var keyValuePairs = new List<KeyValuePair<string, string?>>()
            {
                new("Jwt:Issuer", "TestIssuer"),
                new("Jwt:Audience", "TestAudience"),
                new("Jwt:SecretKey", "TestSecretKeyTestSecretKeyTestSecretKey"),
                new("Jwt:AccessTokenValidityInMinutes", "5"),
                new("Jwt:RefreshTokenValidityInDays", "1"),
            };

            configBuilder.AddInMemoryCollection(keyValuePairs);

            return configBuilder.Build();
        }
    }
}
