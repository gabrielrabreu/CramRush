using Cramming.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cramming.IntegrationTests.Data
{
    public class EfTestFixture
    {
        public readonly AppDbContext Db;

        public EfTestFixture()
        {
            var options = CreateNewContextOptions();

            Db = new AppDbContext(options);
        }

        public void ClearDatabase()
        {
            Db.Database.EnsureDeleted();
        }

        private static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
