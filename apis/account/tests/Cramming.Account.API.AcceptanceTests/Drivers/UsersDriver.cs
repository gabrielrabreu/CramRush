using Cramming.Account.API.AcceptanceTests.Support;
using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Models;

namespace Cramming.Account.API.AcceptanceTests.Drivers
{
    public class UsersDriver(WebApplicationContext webApplicationContext)
    {
        public ActionAttempt<SignUpCommand, WebApiResponse<DomainResult>> SignUp =
            ActionAttemptFactory.Create<SignUpCommand, WebApiResponse<DomainResult>>(
                input => webApplicationContext.ExecutePostAsync<DomainResult>("/api/Users/signup", input));

        public ActionAttempt<SignInCommand, WebApiResponse<TokenResult>> SignIn =
            ActionAttemptFactory.Create<SignInCommand, WebApiResponse<TokenResult>>(
                input => webApplicationContext.ExecutePostAsync<TokenResult>("/api/Users/signin", input));

        public ActionAttempt<WebApiResponse> SignOut =
            ActionAttemptFactory.Create(
                () => webApplicationContext.ExecutePostAsync("/api/Users/signout"));
    }
}
