using Cramming.Account.API.AcceptanceTests.Drivers;
using Cramming.Account.API.AcceptanceTests.Support;
using Cramming.Account.Application.Commands.RefreshToken;
using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Commands.SignUp;
using FluentAssertions;
using Reqnroll;

namespace Cramming.Account.API.AcceptanceTests.Steps.Users
{
    [Binding]
    internal class RefreshTokenStepDefinitions(UsersDriver driver, ServiceProviderContext serviceProviderContext)
    {
        [When("the user submits a request to refresh token")]
        public async Task WhenTheUserSubmitsARequestToRefreshToken()
        {
            await serviceProviderContext.Sender.Send(new SignUpCommand
            {
                UserName = "Username1",
                Email = "username@localhost",
                Password = "Password!123"
            });

            var signInResult = await serviceProviderContext.Sender.Send(new SignInCommand
            {
                UserName = "Username1",
                Password = "Password!123"
            });

            await driver.RefreshToken.PerformAsync(new RefreshTokenCommand
            {
                AccessToken = signInResult.AccessToken,
                RefreshToken = signInResult.RefreshToken
            });
        }

        [Then("the response should contain a new access token")]
        public void ThenTheResponseShouldContainANewAccessToken()
        {
            driver.RefreshToken.LastResult.Should().NotBeNull();
            driver.RefreshToken.LastResult!.ResponseData.Should().NotBeNull();
            driver.RefreshToken.LastResult.ResponseData!.AccessToken.Should().NotBeNull();
            driver.RefreshToken.LastResult.ResponseData!.RefreshToken.Should().NotBeNull();
            driver.RefreshToken.LastResult.ResponseData!.Expiration.Should().BeAfter(DateTime.UtcNow);
        }
    }
}
