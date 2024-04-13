using Cramming.UseCases.Topics.GetNotecards;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.Topics
{
    public class GetNotecards : EndpointBase
    {
        public const string Route = "/Topics/{TopicId}:Notecards";

        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetNotecards))
                .WithTags("Topics")
                .WithSummary("Retrieve notecards for a topic by ID");
        }

        private async Task<Results<FileContentHttpResult, NotFound>> HandleAsync(
            Guid topicId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new GetNotecardsQuery(topicId);

            var result = await mediator.Send(query, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.File(
                result.Value!.Content,
                result.Value.Type,
                result.Value.Name);
        }
    }
}
