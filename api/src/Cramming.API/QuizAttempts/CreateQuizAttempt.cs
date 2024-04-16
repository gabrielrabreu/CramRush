using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.Create;

namespace Cramming.API.QuizAttempts
{
    public class CreateQuizAttempt : EndpointBase
    {
        public const string Route = "/QuizAttempts";

        public static string BuildRoute() => Route;

        public override void Configure(WebApplication app)
        {
            app.MapPost(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(CreateQuizAttempt))
                .WithTags("QuizAttempts")
                .WithSummary("Creates Quiz Attempt");
        }

        private async Task<Results<Created<QuizAttemptDto>, NotFound>> HandleAsync(
            [FromBody] CreateQuizAttemptRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new CreateQuizAttemptCommand(request.QuizId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.Created(GetQuizAttempt.BuildRoute(result.Value!.Id), result.Value);
        }
    }
}
