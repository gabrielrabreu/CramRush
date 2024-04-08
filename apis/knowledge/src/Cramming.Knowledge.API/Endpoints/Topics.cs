using Cramming.Knowledge.API.Infrastructure;
using Cramming.Knowledge.Application.Common.Interfaces;

namespace Cramming.Knowledge.API.Endpoints
{
    public class Topics : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapPost(Testing, "/testing")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization(e => e.RequireRole("Administrator"))
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Summary",
                    Description = "Description."
                });
        }

        public async Task<IResult> Testing(IHttpSession httpSession)
        {
            var teste = httpSession.UserName;
            return Results.NoContent();
        }
    }
}
