using Cramming.Account.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cramming.Account.Infrastructure.Services
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public JwtSecurityToken CreateToken(IList<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:SecretKey")!));
            var token = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("Jwt:Issuer"),
                expires: DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:AccessTokenValidityInMinutes")),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public (ClaimsPrincipal Principal, string Identifier) GetPrincipalFromExpiredToken(string? token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:SecretKey")!)),
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken _);
                var identifier = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                return identifier == null ? throw new UnauthorizedAccessException() : ((ClaimsPrincipal Principal, string Identifier))(principal, identifier);
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }
        }

        public DateTime RefreshTokenExpiryTime() => DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenValidityInDays"));
    }
}
