using FluentValidation;

namespace Cramming.Account.Application.Commands.SignOut
{
    public class SignOutCommandValidator : AbstractValidator<SignOutCommand>
    {
        public SignOutCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty();
        }
    }
}
