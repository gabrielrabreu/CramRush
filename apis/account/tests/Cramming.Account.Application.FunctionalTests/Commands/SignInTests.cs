using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Exceptions;
using Cramming.Account.Application.FunctionalTests.Support;
using FluentAssertions;

namespace Cramming.Account.Application.FunctionalTests.Commands
{
    public class SignInTests(BaseTestFixture fixture) : IClassFixture<BaseTestFixture>
    {
        [Fact]
        public async Task Send_WhenInvalidCommand_ShouldThrowValidationException()
        {
            // Arrange
            var command = new SignInCommand();

            // Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Send_WhenSignInSucceed_ShouldReturnResult()
        {
            // Arrange
            var command = new SignInCommand
            {
                UserName = "Username",
                Password = "Username!123"
            };

            await fixture.SendAsync(new SignUpCommand() { UserName = command.UserName, Email = command.UserName, Password = command.Password });

            // Act
            var result = await fixture.SendAsync(command);

            // Then
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().NotBeNull();
            result.Expiration.Should().BeAfter(DateTime.UtcNow);
        }

        [Fact]
        public async Task Send_WhenSignInFailed_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var command = new SignInCommand
            {
                UserName = "NonExisting",
                Password = "Username!123"
            };

            // Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
