using Cramming.Account.Application.Commands.SignIn;
using MediatR;

namespace Cramming.Account.Application.Commands.RefreshToken
{
    public record RefreshTokenCommand : IRequest<TokenResult>
    {
        public string? AccessToken { get; init; }
        public string? RefreshToken { get; init; }
    }
}
