using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Exceptions;
using Cramming.Account.Application.FunctionalTests.Support;
using FluentAssertions;

namespace Cramming.Account.Application.FunctionalTests.Commands
{
    public class SignUpTests(BaseTestFixture fixture) : IClassFixture<BaseTestFixture>
    {
        [Fact]
        public async Task Send_WhenInvalidCommand_ThenShouldThrowValidationException()
        {
            // Arrange
            var command = new SignUpCommand();

            // Act, Assert
            await FluentActions.Invoking(() => fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Send_WhenCreateUserSucceed_ShouldHaveCreatedItAndReturnResult()
        {
            // Arrange
            var command = new SignUpCommand
            {
                UserName = "Username1",
                Email = "username1@localhost",
                Password = "Username!123"
            };

            // Act
            var result = await fixture.SendAsync(command);

            // Then
            result.Succeeded.Should().BeTrue();

            var user = await fixture.FindUserAsync(command.UserName);
            user.Should().NotBeNull();
            user!.UserName.Should().Be(command.UserName);
            user.Email.Should().Be(command.Email);
        }

        [Fact]
        public async Task Send_WhenCreateUserFailed_ShouldJustReturnResult()
        {
            // Arrange
            var command = new SignUpCommand
            {
                UserName = "Username2",
                Email = "username2@localhost",
                Password = "Username"
            };

            // Act
            var result = await fixture.SendAsync(command);

            // Assert
            result.Succeeded.Should().BeFalse();

            var user = await fixture.FindUserAsync(command.UserName);
            user.Should().BeNull();
        }
    }
}
