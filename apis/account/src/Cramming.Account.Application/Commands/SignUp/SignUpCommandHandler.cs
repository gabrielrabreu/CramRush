using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Events;
using MediatR;

namespace Cramming.Account.Application.Commands.SignUp
{
    public class SignUpCommandHandler(IIdentityService identityService, IPublisher publisher) : IRequestHandler<SignUpCommand, IDomainResult>
    {
        public async Task<IDomainResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var (result, user) = await identityService.CreateAsync(request.UserName!, request.Email!, request.Password!);

            if (result.Succeeded)
                await publisher.Publish(new SignedUpEvent(user.UserName!), cancellationToken);

            return result;
        }
    }
}
