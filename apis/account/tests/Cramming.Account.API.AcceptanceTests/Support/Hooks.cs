using Reqnroll;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    [Binding]
    public class Hooks(DatabaseContext databaseContext)
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            databaseContext.Initialise();
        }
    }

}
