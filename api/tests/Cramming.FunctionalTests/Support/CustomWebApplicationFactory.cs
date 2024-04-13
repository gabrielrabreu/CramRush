using Cramming.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Cramming.FunctionalTests.Support
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string InMemoryId = Guid.NewGuid().ToString();

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            var host = builder.Build();
            host.Start();
            return host;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    services
                        .RemoveAll<DbContextOptions<AppDbContext>>()
                        .AddDbContext<AppDbContext>((sp, options) =>
                        {
                            options.UseInMemoryDatabase(InMemoryId);
                        });
                });
        }
    }
}
