using Cramming.Account.Application.Commands.SignUp;
using FluentValidation.TestHelper;

namespace Cramming.Account.Application.UnitTests.Commands.SignUp
{
    public class SignUpCommandValidatorTests
    {
        private readonly SignUpCommandValidator _validator;

        public SignUpCommandValidatorTests()
        {
            _validator = new SignUpCommandValidator();
        }

        [Fact]
        public void Validate_WhenUserNameIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new SignUpCommand() { UserName = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.UserName)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenUserNameIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new SignUpCommand() { UserName = "UserName" });
            result.ShouldNotHaveValidationErrorFor(command => command.UserName);
        }

        [Fact]
        public void Validate_WhenEmailIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new SignUpCommand() { Email = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.Email)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenEmailIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new SignUpCommand() { Email = "email@localhost" });
            result.ShouldNotHaveValidationErrorFor(command => command.Email);
        }

        [Fact]
        public void Validate_WhenPasswordIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new SignUpCommand() { Password = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.Password)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenPasswordIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new SignUpCommand() { Password = "Password!123" });
            result.ShouldNotHaveValidationErrorFor(command => command.Password);
        }
    }
}
