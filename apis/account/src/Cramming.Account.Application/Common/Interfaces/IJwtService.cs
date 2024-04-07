using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cramming.Account.Application.Common.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken CreateToken(IList<Claim> claims);
        string GenerateRefreshToken();
        (ClaimsPrincipal Principal, string Identifier) GetPrincipalFromExpiredToken(string? token);
        DateTime RefreshTokenExpiryTime();
    }
}
