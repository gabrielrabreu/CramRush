using Cramming.Account.Application.Commands.SignIn;
using FluentValidation.TestHelper;

namespace Cramming.Account.Application.UnitTests.Commands.SignIn
{
    public class SignInCommandValidatorTests
    {
        private readonly SignInCommandValidator _validator;

        public SignInCommandValidatorTests()
        {
            _validator = new SignInCommandValidator();
        }

        [Fact]
        public void Validate_WhenUserNameIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new SignInCommand() { UserName = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.UserName)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenUserNameIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new SignInCommand() { UserName = "UserName" });
            result.ShouldNotHaveValidationErrorFor(command => command.UserName);
        }

        [Fact]
        public void Validate_WhenPasswordIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new SignInCommand() { Password = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.Password)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenPasswordIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new SignInCommand() { Password = "Password!123" });
            result.ShouldNotHaveValidationErrorFor(command => command.Password);
        }
    }
}
