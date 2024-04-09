using Cramming.API.Endpoints.Extensions;
using Cramming.Application.Notecards.Queries;
using MediatR;

namespace Cramming.API.Endpoints
{
    public class Notecards : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapGet(GetNotecardsByTopic, "{topicId}")
                .Produces(StatusCodes.Status200OK)
                .WithOpenApi(operation =>
                {
                    operation.Parameters[0].Description = "The ID of the topic for which to retrieve Notecards.";
                    return new(operation)
                    {
                        Summary = "Retrieve Notecards for a specific topic",
                        Description = "Endpoint to retrieve Notecards associated with a specific topic in the application."
                    };
                });
        }

        public async Task<IResult> GetNotecardsByTopic(ISender sender, Guid topicId)
        {
            var fileComposed = await sender.Send(new GetNotecardsByTopicQuery(topicId));
            return Results.File(fileComposed.Content, fileComposed.ContentType, fileComposed.Name);
        }
    }
}
