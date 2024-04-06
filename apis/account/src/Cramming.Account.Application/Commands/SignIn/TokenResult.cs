namespace Cramming.Account.Application.Commands.SignIn
{
    public record TokenResult(string AccessToken, string RefreshToken, DateTime Expiration);
}
