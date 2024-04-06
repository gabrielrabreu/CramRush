using Cramming.Account.API.AcceptanceTests.Drivers;
using Cramming.Account.Application.Commands.SignUp;
using FluentAssertions;
using Reqnroll;

namespace Cramming.Account.API.AcceptanceTests.Steps.Users
{
    [Binding]
    internal class SignUpStepDefinitions(UsersDriver driver)
    {
        [When("the user submits a request to signup with UserName {string}, Email {string} and Password {string}")]
        public async Task WhenTheUserSubmitsARequestToSignupWithUserNameEmailAndPassword(string userName, string email, string password)
        {
            await driver.SignUp.PerformAsync(new SignUpCommand
            {
                UserName = userName,
                Email = email,
                Password = password
            });
        }

        [Then("the response for signup should contain the error list")]
        public void ThenTheResponseForSignupShouldContainTheErrorList()
        {
            driver.SignUp.LastResult.Should().NotBeNull();
            driver.SignUp.LastResult!.ResponseData.Should().NotBeNull();
            driver.SignUp.LastResult.ResponseData!.Succeeded.Should().BeFalse();
            driver.SignUp.LastResult.ResponseData!.Errors.Should().HaveCountGreaterThan(0);
        }
    }
}
