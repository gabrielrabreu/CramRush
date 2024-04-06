using Cramming.Account.API.Infrastructure;
using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Models;
using Cramming.Account.Application.Queries.GetUsers;
using MediatR;

namespace Cramming.Account.API.Endpoints
{
    public class Users : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapPost(SignUp, "/signup")
                .Produces(StatusCodes.Status204NoContent)
                .Produces<DomainResult>(StatusCodes.Status400BadRequest)
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "User Sign Up",
                    Description = "Registers a new user in the system."
                });

            group.MapGet(GetUsers)
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Get Paginated Users",
                    Description = "Retrieves a paginated list of all users registered in the system."
                });
        }

        public async Task<IResult> SignUp(ISender sender, SignUpCommand command)
        {
            var result = await sender.Send(command);
            if (result.Succeeded) return Results.NoContent();
            return Results.BadRequest(result);
        }

        public async Task<PaginatedList<UserBriefDto>> GetUsers(ISender sender, [AsParameters] GetUsersQuery query)
        {
            return await sender.Send(query);
        }
    }
}
