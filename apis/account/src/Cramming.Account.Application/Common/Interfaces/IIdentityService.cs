using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Entities;
using System.Security.Claims;

namespace Cramming.Account.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        IQueryable<IApplicationUser> Users { get; }

        Task<IApplicationUser?> FindByIdAsync(string userId);

        Task<IList<Claim>> ListClaimsAsync(Guid userId);

        Task<(bool Succeed, IApplicationUser? User)> AuthenticateAsync(string userName, string password);

        Task<(IDomainResult Result, IApplicationUser User)> CreateAsync(string username, string email, string password);

        Task UpdateRefreshTokenAsync(string userId, string? newRefreshToken, DateTime? newRefreshTokenExpiryTime);
    }
}
