using Cramming.UseCases.Topics.UpdateTag;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cramming.API.Topics
{
    public class UpdateTag : EndpointBase
    {
        public override void Configure(WebApplication app)
        {
            app.MapPut(UpdateTagRequest.Route, HandleAsync)
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
