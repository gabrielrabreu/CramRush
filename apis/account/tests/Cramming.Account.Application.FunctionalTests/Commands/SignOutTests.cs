using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Commands.SignOut;
using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Exceptions;
using Cramming.Account.Application.FunctionalTests.Support;
using FluentAssertions;

namespace Cramming.Account.Application.FunctionalTests.Commands
{
    public class SignOutTests(BaseTestFixture fixture) : IClassFixture<BaseTestFixture>
    {
        [Fact]
        public async Task Send_WhenInvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var command = new SignOutCommand(string.Empty);

            // Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Send_WhenSignOutSucceed_ShouldRevokeRefreshTokens()
        {
            // Arrange
            var userName = "UserName";
            var password = "UserName!123";

            await fixture.SendAsync(new SignUpCommand() { UserName = userName, Email = userName, Password = password });
            await fixture.SendAsync(new SignInCommand() { UserName = userName, Password = password });

            var userBeforeSignOut = await fixture.FindUserAsync(userName);

            // Act
            await fixture.SendAsync(new SignOutCommand(userBeforeSignOut!.Id.ToString()));

            // Then
            var userAfterSignOut = await fixture.FindUserAsync(userName);
            userAfterSignOut!.RefreshToken.Should().BeNull();
            userAfterSignOut.RefreshTokenExpiryTime.Should().BeNull();
        }

        [Fact]
        public async Task Send_WhenSignOutWithNonExistingUser_ShouldDoNothing()
        {
            // Arrange, Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(new SignOutCommand(Guid.NewGuid().ToString()))).Should().NotThrowAsync();
        }
    }
}
