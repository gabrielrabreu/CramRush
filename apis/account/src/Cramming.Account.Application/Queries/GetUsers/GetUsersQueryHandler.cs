using Cramming.Account.Application.Common.Extensions;
using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.Common.Models;
using MediatR;

namespace Cramming.Account.Application.Queries.GetUsers
{
    public class GetUsersQueryHandler(IIdentityService identityService) : IRequestHandler<GetUsersQuery, PaginatedList<UserBriefDto>>
    {
        public async Task<PaginatedList<UserBriefDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = identityService.Users
                .OrderBy(x => x.UserName)
                .Select(x => new UserBriefDto(x.Id, x.UserName, x.Email));
            return await identityService.Users
                .OrderBy(x => x.UserName)
                .Select(x => new UserBriefDto(x.Id, x.UserName, x.Email))
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
