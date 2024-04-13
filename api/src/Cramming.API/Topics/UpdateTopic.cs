using Cramming.UseCases.Topics.Update;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cramming.API.Topics
{
    public class UpdateTopic : EndpointBase
    {
        public const string Route = "/Topics/{TopicId}";
        
        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapPut(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(UpdateTopic))
                .WithTags("Topics")
                .WithSummary("Updates a topic by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            [FromBody] UpdateTopicRequest request,
            Guid topicId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new UpdateTopicCommand(topicId, request.Name);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
