using Cramming.UseCases.Topics.UpdateTag;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cramming.API.Topics
{
    public class UpdateTag : EndpointBase
    {
        public const string Route = "/Topics/{TopicId}/Tags/{TagId}";

        public static string BuildRoute(Guid topicId, Guid tagId)
            => Route
                   .Replace("{TopicId}", topicId.ToString())
                   .Replace("{TagId}", tagId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapPut(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(UpdateTag))
                .WithTags("Topics")
                .WithSummary("Updates a tag by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            [FromBody] UpdateTagRequest request,
            Guid topicId,
            Guid tagId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new UpdateTagCommand(tagId, topicId, request.Name, request.Colour);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
