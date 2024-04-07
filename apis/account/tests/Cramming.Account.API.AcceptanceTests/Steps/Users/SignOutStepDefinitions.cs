using Cramming.Account.API.AcceptanceTests.Drivers;
using Reqnroll;

namespace Cramming.Account.API.AcceptanceTests.Steps.Users
{
    [Binding]
    internal class SignOutStepDefinitions(UsersDriver driver)
    {
        [When("the user submits a request to sign out")]
        public async Task WhenTheUserSubmitsARequestToSignOut()
        {
            await driver.SignOut.PerformAsync();
        }
    }
}
