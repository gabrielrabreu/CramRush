namespace Cramming.Account.Application.FunctionalTests.Support.Data
{
    public static class TestDatabaseFactory
    {
        public static ITestDatabase Create()
        {
            var database = new SqliteTestDatabase();
            database.Initialise();
            return database;
        }
    }
}
