using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Entities;

namespace Cramming.Account.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        IQueryable<IApplicationUser> Users { get; }

        Task<(IDomainResult Result, IApplicationUser User)> CreateAsync(string username, string email, string password);
    }
}
