using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Domain.Events;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Cramming.Account.Application.Commands.SignIn
{
    public class SignInCommandHandler(IIdentityService identityService, IJwtService jwtService, IPublisher publisher) : IRequestHandler<SignInCommand, TokenResult>
    {
        public async Task<TokenResult> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var (succeed, user) = await identityService.AuthenticateAsync(request.UserName!, request.Password!);

            if (succeed)
            {
                var claims = await identityService.ListClaimsAsync(user!.Id);

                var accessToken = jwtService.CreateToken(claims);
                var refreshToken = jwtService.GenerateRefreshToken();
                var refreshTokenExpiryTime = jwtService.RefreshTokenExpiryTime();

                await identityService.UpdateRefreshTokenAsync(user.Id.ToString(), refreshToken, refreshTokenExpiryTime);

                await publisher.Publish(new SignedInEvent(request.UserName!), cancellationToken);

                return new TokenResult(new JwtSecurityTokenHandler().WriteToken(accessToken),
                                       refreshToken,
                                       accessToken.ValidTo);
            }

            throw new UnauthorizedAccessException();
        }
    }
}
