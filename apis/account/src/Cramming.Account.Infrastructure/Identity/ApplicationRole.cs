using Cramming.Account.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cramming.Account.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<Guid>, IApplicationRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
