using Cramming.Account.Application.Commands.SignOut;
using FluentValidation.TestHelper;

namespace Cramming.Account.Application.UnitTests.Commands.SignOut
{
    public class SignOutCommandValidatorTests
    {
        private readonly SignOutCommandValidator _validator;

        public SignOutCommandValidatorTests()
        {
            _validator = new SignOutCommandValidator();
        }

        [Fact]
        public void Validate_WhenUserIdIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new SignOutCommand(string.Empty));
            result.ShouldHaveValidationErrorFor(command => command.UserId)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenUserIdIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new SignOutCommand("UserId"));
            result.ShouldNotHaveValidationErrorFor(command => command.UserId);
        }
    }
}
