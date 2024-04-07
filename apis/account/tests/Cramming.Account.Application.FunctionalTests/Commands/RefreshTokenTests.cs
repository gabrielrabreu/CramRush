using Cramming.Account.Application.Commands.RefreshToken;
using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Exceptions;
using Cramming.Account.Application.FunctionalTests.Support;
using FluentAssertions;

namespace Cramming.Account.Application.FunctionalTests.Commands
{
    public class RefreshTokenTests(BaseTestFixture fixture) : IClassFixture<BaseTestFixture>
    {
        [Fact]
        public async Task Send_WhenInvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var command = new RefreshTokenCommand();

            // Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Send_WhenRefreshSucceed_ShouldReturnResult()
        {
            // Arrange
            var userName = "UserName";
            var password = "UserName!123";

            await fixture.SendAsync(new SignUpCommand() { UserName = userName, Email = userName, Password = password });
            var signInResult = await fixture.SendAsync(new SignInCommand() { UserName = userName, Password = password });

            // Act
            var result = await fixture.SendAsync(new RefreshTokenCommand { AccessToken = signInResult.AccessToken, RefreshToken = signInResult.RefreshToken });

            // Then
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().NotBeNull();
            result.Expiration.Should().BeAfter(DateTime.UtcNow);

            var userAfterSignIn = await fixture.FindUserAsync(userName);
            userAfterSignIn!.RefreshToken.Should().NotBeNull();
            userAfterSignIn!.RefreshToken.Should().NotBe(signInResult.RefreshToken);
            userAfterSignIn.RefreshTokenExpiryTime.Should().NotBeNull();
        }

        [Fact]
        public async Task Send_WhenRefreshFailed_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var command = new RefreshTokenCommand
            {
                AccessToken = "AccessToken",
                RefreshToken = "RefreshToken"
            };

            // Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
