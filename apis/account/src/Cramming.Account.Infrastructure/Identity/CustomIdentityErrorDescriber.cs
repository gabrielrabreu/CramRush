using Cramming.Account.Domain.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Cramming.Account.Infrastructure.Identity
{
    public class CustomIdentityErrorDescriber(IStringLocalizer<Resource> stringLocalizer) : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = "Password",
                Description = stringLocalizer["PasswordRequiresNonAlphanumeric"]
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = "Password",
                Description = stringLocalizer["PasswordRequiresDigit"]
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = "Password",
                Description = stringLocalizer["PasswordRequiresUpper"]
            };
        }
    }
}
