using Cramming.UseCases.Topics.Delete;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.Topics
{
    public class DeleteTopic : EndpointBase
    {
        public const string Route = "/Topics/{TopicId}";

        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapDelete(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DeleteTopic))
                .WithTags("Topics")
                .WithSummary("Deletes a topic by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            Guid topicId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new DeleteTopicCommand(topicId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
