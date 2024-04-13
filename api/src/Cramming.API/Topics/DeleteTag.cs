using Cramming.UseCases.Topics.DeleteTag;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.Topics
{
    public class DeleteTag : EndpointBase
    {
        public const string Route = "/Topics/{TopicId}/Tags/{TagId}";

        public static string BuildRoute(Guid topicId, Guid tagId)
            => Route
                   .Replace("{TopicId}", topicId.ToString())
                   .Replace("{TagId}", tagId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapDelete(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DeleteTag))
                .WithTags("Topics")
                .WithSummary("Deletes a tag by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            Guid topicId,
            Guid tagId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new DeleteTagCommand(tagId, topicId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
