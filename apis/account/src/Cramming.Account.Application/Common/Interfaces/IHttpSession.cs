namespace Cramming.Account.Application.Common.Interfaces
{
    public interface IHttpSession
    {
        string? UserId { get; }
        string? UserName { get; }
    }
}
