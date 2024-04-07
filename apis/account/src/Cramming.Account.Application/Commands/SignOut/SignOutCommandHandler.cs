using Cramming.Account.Application.Common.Interfaces;
using MediatR;

namespace Cramming.Account.Application.Commands.SignOut
{
    public class SignOutCommandHandler(IIdentityService identityService) : IRequestHandler<SignOutCommand>
    {
        public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await identityService.UpdateRefreshTokenAsync(request.UserId, null, null);
        }
    }
}
