using MediatR;

namespace Cramming.Account.Application.Commands.SignOut
{
    /// <summary>
    /// Command used for signing out.
    /// </summary>
    public record SignOutCommand(string UserId) : IRequest
    {
    }
}
