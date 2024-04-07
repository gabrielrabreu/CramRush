using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Cramming.Account.Infrastructure.Identity
{
    public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
    {
        public IQueryable<IApplicationUser> Users
        {
            get
            {
                return userManager.Users;
            }
        }

        public async Task<(IDomainResult Result, IApplicationUser User)> CreateAsync(string username, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user);
        }

        public async Task<bool> CheckPasswordAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            return user != null && await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<Claim>> GetUserClaimsAsync(string username)
        {
            var claims = new List<Claim>();

            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                claims.Add(new(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new(ClaimTypes.Name, user.UserName!));

                var roles = await userManager.GetRolesAsync(user);

                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task UpdateRefreshToken(string username, string newRefreshToken, DateTime newRefreshTokenExpiryTime)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = newRefreshTokenExpiryTime;
                await userManager.UpdateAsync(user);
            }
        }

        public async Task RevokeRefreshToken(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await userManager.UpdateAsync(user);
            }
        }
    }
}
