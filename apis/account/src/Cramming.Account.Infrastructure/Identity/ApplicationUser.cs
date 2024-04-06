using Cramming.Account.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cramming.Account.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
