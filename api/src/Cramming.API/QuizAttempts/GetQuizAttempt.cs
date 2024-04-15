using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.Get;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.QuizAttempts
{
    public class GetQuizAttempt : EndpointBase
    {
        public const string Route = "/QuizAttempts/{QuizAttemptId}";

        public static string BuildRoute(Guid quizAttemptId) => Route.Replace("{QuizAttemptId}", quizAttemptId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetQuizAttempt))
                .WithTags("QuizAttempts")
                .WithSummary("Gets Quiz Attempt by its ID");
        }

        private async Task<Results<Ok<QuizAttemptDto>, NotFound>> HandleAsync(
            Guid QuizAttemptDto,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new GetQuizAttemptQuery(QuizAttemptDto);

            var result = await mediator.Send(query, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.Ok(result.Value);
        }
    }
}
