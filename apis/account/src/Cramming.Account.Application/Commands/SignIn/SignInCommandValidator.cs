using FluentValidation;

namespace Cramming.Account.Application.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty();

            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}
