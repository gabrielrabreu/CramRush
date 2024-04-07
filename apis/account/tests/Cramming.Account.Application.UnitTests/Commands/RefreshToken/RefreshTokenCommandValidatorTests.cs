using Cramming.Account.Application.Commands.RefreshToken;
using FluentValidation.TestHelper;

namespace Cramming.Account.Application.UnitTests.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests
    {
        private readonly RefreshTokenCommandValidator _validator;

        public RefreshTokenCommandValidatorTests()
        {
            _validator = new RefreshTokenCommandValidator();
        }

        [Fact]
        public void Validate_WhenAccessTokenIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new RefreshTokenCommand() { AccessToken = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.AccessToken)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenAccessTokenIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new RefreshTokenCommand() { AccessToken = "AccessToken" });
            result.ShouldNotHaveValidationErrorFor(command => command.AccessToken);
        }

        [Fact]
        public void Validate_WhenRefreshTokenIsEmpty_ShouldHaveError()
        {
            var result = _validator.TestValidate(new RefreshTokenCommand() { RefreshToken = string.Empty });
            result.ShouldHaveValidationErrorFor(command => command.RefreshToken)
                .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validate_WhenRefreshTokenIsProvided_ShouldNotHaveError()
        {
            var result = _validator.TestValidate(new RefreshTokenCommand() { RefreshToken = "RefreshToken" });
            result.ShouldNotHaveValidationErrorFor(command => command.RefreshToken);
        }
    }
}
