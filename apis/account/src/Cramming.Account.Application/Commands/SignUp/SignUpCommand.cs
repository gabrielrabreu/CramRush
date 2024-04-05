using Cramming.Account.Application.Common.Models;
using MediatR;

namespace Cramming.Account.Application.Commands.SignUp
{
    /// <summary>
    /// Command used for signing up a new user.
    /// </summary>
    public record SignUpCommand : IRequest<IDomainResult>
    {
        /// <summary>
        /// Gets or initializes the username of the user.
        /// </summary>
        public string? UserName { get; init; }

        /// <summary>
        /// Gets or initializes the email of the user.
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// Gets or initializes the password of the user.
        /// </summary>
        public string? Password { get; init; }
    }
}
