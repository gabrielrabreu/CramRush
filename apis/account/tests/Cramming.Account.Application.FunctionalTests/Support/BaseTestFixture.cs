using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.FunctionalTests.Support.Data;
using Cramming.Account.Domain.Entities;
using Cramming.Account.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cramming.Account.Application.FunctionalTests.Support
{
    public class BaseTestFixture : IDisposable
    {
        public ITestDatabase Database { get; private set; }
        public IServiceScopeFactory ScopeFactory { get; private set; }

        public BaseTestFixture()
        {
            Database = TestDatabaseFactory.Create();
            ScopeFactory = TestScopeFactory.Create(Database);
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = ScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
            return await mediator.Send(request);
        }

        public async Task SendAsync(IRequest request)
        {
            using var scope = ScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
            await mediator.Send(request);
        }

        public async Task<IApplicationUser?> FindUserAsync(string userName)
        {
            using var scope = ScopeFactory.CreateScope();
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            return await identityService.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public void Dispose()
        {
            using var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Database.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
