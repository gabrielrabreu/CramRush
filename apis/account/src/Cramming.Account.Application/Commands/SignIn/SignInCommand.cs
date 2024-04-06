using MediatR;

namespace Cramming.Account.Application.Commands.SignIn
{
    /// <summary>
    /// Command used for signing in.
    /// </summary>
    public record SignInCommand : IRequest<TokenResult>
    {
        /// <summary>
        /// Gets or initializes the username of the user.
        /// </summary>
        public string? UserName { get; init; }

        /// <summary>
        /// Gets or initializes the password of the user.
        /// </summary>
        public string? Password { get; init; }
    }
}
