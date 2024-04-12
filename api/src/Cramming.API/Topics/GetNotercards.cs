using Cramming.UseCases.Topics.GetNotecards;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.Topics
{
    public class GetNotercards : EndpointBase
    {
        public override void Configure(WebApplication app)
        {
            app.MapGet(GetNotecardsRequest.Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetNotercards))
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
                result.Value.Content,
                result.Value.ContentType,
                result.Value.Name);
        }
    }
}
