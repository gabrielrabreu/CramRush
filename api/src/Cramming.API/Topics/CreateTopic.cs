using Cramming.UseCases.Topics;
using Cramming.UseCases.Topics.Create;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Cramming.API.Topics
{
    public class CreateTopic : EndpointBase
    {
        public const string Route = "/Topics";

        public override void Configure(WebApplication app)
        {
            app.MapPost(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(CreateTopic))
                .WithTags("Topics")
                .WithSummary("Create a new topic");
        }

        private async Task<Created<TopicBriefDto>> HandleAsync(
            [FromBody] CreateTopicRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new CreateTopicCommand(request.Name);

            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Created(GetTopicById.BuildRoute(result.Value!.Id), result.Value);
        }
    }
}
