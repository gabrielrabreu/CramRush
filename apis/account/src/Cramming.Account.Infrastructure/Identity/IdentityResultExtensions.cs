using Cramming.Account.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Cramming.Account.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static DomainResult ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? DomainResult.Success()
                : DomainResult.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
