using Cramming.Account.API.AcceptanceTests.Drivers;
using Cramming.Account.API.AcceptanceTests.Support;
using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Commands.SignUp;
using FluentAssertions;
using Reqnroll;

namespace Cramming.Account.API.AcceptanceTests.Steps.Users
{
    [Binding]
    internal class SignInStepDefinitions(UsersDriver driver, ServiceProviderContext serviceProviderContext)
    {
        [Given("there exists an user with UserName {string} and Password {string}")]
        public async Task GivenThereExistsAnUserWithUserNameAndPassword(string userName, string password)
        {
            await serviceProviderContext.Sender.Send(new SignUpCommand
            {
                UserName = userName,
                Email = userName,
                Password = password
            });
        }

        [When("the user submits a request to sign in with UserName {string} and Password {string}")]
        public async Task WhenTheUserSubmitsARequestToSignInWithUserNameAndPassword(string userName, string password)
        {
            await driver.SignIn.PerformAsync(new SignInCommand
            {
                UserName = userName,
                Password = password
            });
        }

        [Then("the response should contain the user's access token")]
        public void ThenTheResponseShouldContainTheUsersAccessToken()
        {
            driver.SignIn.LastResult.Should().NotBeNull();
            driver.SignIn.LastResult!.ResponseData.Should().NotBeNull();
            driver.SignIn.LastResult.ResponseData!.AccessToken.Should().NotBeNull();
            driver.SignIn.LastResult.ResponseData!.RefreshToken.Should().NotBeNull();
            driver.SignIn.LastResult.ResponseData!.Expiration.Should().BeAfter(DateTime.UtcNow);
        }
    }
}
