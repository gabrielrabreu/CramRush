using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Entities;
using System.Security.Claims;

namespace Cramming.Account.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        IQueryable<IApplicationUser> Users { get; }

        Task<bool> CheckPasswordAsync(string username, string password);
        Task<IList<Claim>> GetUserClaimsAsync(string username);

        Task<(IDomainResult Result, IApplicationUser User)> CreateAsync(string username, string email, string password);
        Task UpdateRefreshToken(string username, string newRefreshToken, DateTime newRefreshTokenExpiryTime);
    }
}
