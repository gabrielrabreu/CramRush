using Cramming.Account.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class DatabaseContext : IDisposable
    {
        private readonly string _connectionString;
        private readonly SqliteConnection _connection;

        public DatabaseContext()
        {
            _connectionString = "DataSource=:memory:";
            _connection = new SqliteConnection(_connectionString);
        }

        public void Initialise()
        {
            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.Migrate();
        }


        public DbConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
