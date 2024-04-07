using Cramming.Account.API.AcceptanceTests.Support;
using FluentAssertions;
using Reqnroll;
using System.Net;

namespace Cramming.Account.API.AcceptanceTests.Steps
{
    [Binding]
    public class CommonStepDefinitions(WebApplicationContext webApplicationContext, AuthContext authContext)
    {
        [Given(@"the user is authenticated with the scope {string}")]
        public void GivenTheUserIsAuthenticatedWithTheRequiredScope(string scope)
        {
            authContext.AsScopedUser(scope);
        }

        [Given("the user is authenticated")]
        public void GivenTheUserIsAuthenticated()
        {
            authContext.AsDefaultUser();
        }

        [Given("the user is not authenticated")]
        public void GivenTheUserIsNotAuthenticated()
        {
        }

        [Given("the user is authenticated with insufficient scope")]
        public void GivenTheUserIsAuthenticatedWithInsufficientScope()
        {
            authContext.AsDefaultUser();
        }

        [Then("the response status code should be {int}")]
        public void ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            webApplicationContext.WebApiResponse.Should().NotBeNull();
            webApplicationContext.WebApiResponse!.StatusCode.Should().Be((HttpStatusCode)statusCode);
        }
    }
}
