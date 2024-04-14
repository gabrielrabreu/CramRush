using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Get;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.StaticQuizzes
{
    public class GetStaticQuiz : EndpointBase
    {
        public const string Route = "/StaticQuizzes/{StaticQuizId}";

        public static string BuildRoute(Guid staticQuizId) => Route.Replace("{StaticQuizId}", staticQuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetStaticQuiz))
                .WithTags("StaticQuizzes")
                .WithSummary("Retrieve static quiz by ID");
        }

        private async Task<Results<Ok<StaticQuizDto>, NotFound>> HandleAsync(
            Guid staticQuizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new GetStaticQuizQuery(staticQuizId);

            var result = await mediator.Send(query, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.Ok(result.Value);
        }
    }
}
