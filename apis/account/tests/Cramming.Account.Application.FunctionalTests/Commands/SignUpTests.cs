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
        public async Task Send_WhenCreateUserSucceed_ShouldReturnSuceedResult()
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
        }

        [Fact]
        public async Task Send_WhenCreateUserFailed_ShouldReturnFailedResult()
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
        }
    }
}
