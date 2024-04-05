using System.Data.Common;

namespace Cramming.Account.Application.FunctionalTests.Support.Data
{
    public interface ITestDatabase : IDisposable
    {
        void Initialise();

        DbConnection GetConnection();
    }
}
