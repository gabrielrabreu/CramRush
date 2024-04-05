using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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
    }
}
