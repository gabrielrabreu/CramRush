using Cramming.Account.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class ServiceProviderContext(CustomWebApplicationFactory factory)
    {
        private readonly IServiceProvider _serviceProvider = factory.Services.CreateScope().ServiceProvider;

        public ISender Sender
        {
            get
            {
                return _serviceProvider.GetRequiredService<ISender>();
            }
        }

        public IIdentityService IdentityService
        {
            get
            {
                return _serviceProvider.GetRequiredService<IIdentityService>();
            }
        }
    }
}
