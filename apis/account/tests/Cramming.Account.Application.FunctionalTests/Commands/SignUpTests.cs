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
            var userName = "UserName1";
            var email = "userName1@localhost";
            var password = "UserName1!123";

            // Act
            var result = await fixture.SendAsync(new SignUpCommand { UserName = userName, Email = email, Password = password });

            // Then
            result.Succeeded.Should().BeTrue();

            var userAfterSignUp = await fixture.FindUserAsync(userName);
            userAfterSignUp.Should().NotBeNull();
            userAfterSignUp!.Email.Should().Be(email);
            userAfterSignUp.UserName.Should().Be(userName);
        }

        [Fact]
        public async Task Send_WhenCreateUserFailed_ShouldReturnFailedResult()
        {
            // Arrange
            var command = new SignUpCommand
            {
                UserName = "UserName2",
                Email = "userName2@localhost",
                Password = "UserName2"
            };

            // Act
            var result = await fixture.SendAsync(command);

            // Assert
            result.Succeeded.Should().BeFalse();
        }
    }
}
