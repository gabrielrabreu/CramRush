using Cramming.Account.Application.Common.Models;
using MediatR;

namespace Cramming.Account.Application.Queries.GetUsers
{
    /// <summary>
    /// Represents the query parameters for retrieving paginated user data.
    /// </summary>
    public record GetUsersQuery : IRequest<PaginatedList<UserBriefDto>>
    {
        /// <summary>
        /// Gets or sets the page number to retrieve.
        /// </summary>
        /// <remarks>The default value is 1.</remarks>
        public int PageNumber { get; init; } = 1;

        /// <summary>
        /// Gets or sets the number of users per page.
        /// </summary>
        /// <remarks>The default value is 10.</remarks>
        public int PageSize { get; init; } = 10;
    }

}
