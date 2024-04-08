namespace Cramming.Knowledge.Application.Common.Interfaces
{
    public interface IHttpSession
    {
        string? UserId { get; }
        string? UserName { get; }
    }
}
