using Cramming.API.Endpoints.Extensions;
using Cramming.Application.Practices.Queries;
using MediatR;

namespace Cramming.API.Endpoints
{
    public class Practices : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapGet(GetPracticeByTopic, "{topicId}")
                .Produces(StatusCodes.Status200OK)
                .WithOpenApi(operation =>
                {
                    operation.Parameters[0].Description = "The ID of the topic for which to retrieve Practice Test.";
                    return new(operation)
                    {
                        Summary = "Retrieve Practice Test for a specific topic",
                        Description = "Endpoint to retrieve Practice Test associated with a specific topic in the application."
                    };
                });
        }

        public async Task<IResult> GetPracticeByTopic(ISender sender, Guid topicId)
        {
            var fileComposed = await sender.Send(new GetPracticeByTopicQuery(topicId));
            return Results.File(fileComposed.Content, fileComposed.ContentType, fileComposed.Name);
        }
    }
}
