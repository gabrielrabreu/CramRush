using FluentValidation;

namespace Cramming.Account.Application.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty();

            RuleFor(v => v.Email)
                .NotEmpty();

            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}
