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

            services.AddLocalization();

            services.AddLogging(builder => builder.AddConsole());

            services.AddApplicationServices();

            services.AddInfrastructureServices(new ConfigurationBuilder().Build());

            services
                .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((sp, options) =>
                {
                    options.UseSqlite(database.GetConnection());
                });

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }
    }
}
