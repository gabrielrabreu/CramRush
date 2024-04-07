namespace Cramming.Account.Domain.Entities
{
    public interface IApplicationUser
    {
        Guid Id { get; }
        string? UserName { get; set; }
        string? Email { get; set; }
        string? RefreshToken { get; set; }
        DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
