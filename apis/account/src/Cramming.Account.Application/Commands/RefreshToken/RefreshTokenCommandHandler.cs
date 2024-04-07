using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Common.Interfaces;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Cramming.Account.Application.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IIdentityService identityService, IJwtService jwtService) : IRequestHandler<RefreshTokenCommand, TokenResult>
    {
        public async Task<TokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var (principal, identifier) = jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

            var user = await identityService.FindByIdAsync(identifier);
            if (user != null && user.RefreshToken == request.RefreshToken && user.RefreshTokenExpiryTime >= DateTime.UtcNow)
            {
                var accessToken = jwtService.CreateToken(principal.Claims.ToList());
                var refreshToken = jwtService.GenerateRefreshToken();
                var refreshTokenExpiryTime = jwtService.RefreshTokenExpiryTime();

                await identityService.UpdateRefreshTokenAsync(user.Id.ToString(), refreshToken, refreshTokenExpiryTime);

                return new TokenResult(new JwtSecurityTokenHandler().WriteToken(accessToken),
                                        refreshToken,
                                        accessToken.ValidTo);
            }

            throw new UnauthorizedAccessException();
        }
    }
}
