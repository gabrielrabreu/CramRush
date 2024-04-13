using Cramming.UseCases.Topics.GetPracticeTest;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.Topics
{
    public class GetPracticeTest : EndpointBase
    {
        public override void Configure(WebApplication app)
        {
            app.MapGet(GetPracticeTestRequest.Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetPracticeTest))
                .WithTags("Topics")
                .WithSummary("Retrieve practice test for a topic by ID");
        }

        private async Task<Results<FileContentHttpResult, NotFound>> HandleAsync(
            Guid topicId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new GetPracticeTestQuery(topicId);

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
