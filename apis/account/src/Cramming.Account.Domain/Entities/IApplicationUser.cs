namespace Cramming.Account.Domain.Entities
{
    public interface IApplicationUser
    {
        Guid Id { get; }
        string? UserName { get; set; }
        string? Email { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
