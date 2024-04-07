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

        public async Task<IApplicationUser?> FindByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<IList<Claim>> ListClaimsAsync(Guid userId)
        {
            var claims = new List<Claim>();

            var existingUser = await userManager.FindByIdAsync(userId.ToString());
            if (existingUser != null)
            {
                claims.Add(new(ClaimTypes.NameIdentifier, existingUser.Id.ToString()));
                claims.Add(new(ClaimTypes.Name, existingUser.UserName!));

                var roles = await userManager.GetRolesAsync(existingUser);

                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task<(bool Succeed, IApplicationUser? User)> AuthenticateAsync(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            var succeed = user != null && await userManager.CheckPasswordAsync(user, password);
            return (succeed, user);
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

        public async Task UpdateRefreshTokenAsync(string userId, string? newRefreshToken, DateTime? newRefreshTokenExpiryTime)
        {
            var existingUser = await userManager.FindByIdAsync(userId);
            if (existingUser != null)
            {
                existingUser.RefreshToken = newRefreshToken;
                existingUser.RefreshTokenExpiryTime = newRefreshTokenExpiryTime;
                await userManager.UpdateAsync(existingUser);
            }
        }
    }
}
