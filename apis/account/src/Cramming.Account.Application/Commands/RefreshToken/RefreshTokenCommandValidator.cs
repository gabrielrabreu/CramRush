using FluentValidation;

namespace Cramming.Account.Application.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(v => v.AccessToken)
                .NotEmpty();

            RuleFor(v => v.RefreshToken)
                .NotEmpty();
        }
    }
}
