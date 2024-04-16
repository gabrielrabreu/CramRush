using Cramming.UseCases.Quizzes.Get;

namespace Cramming.API.Quizzes
{
    public class GetQuiz : EndpointBase
    {
        public const string Route = "/Quizzes/{QuizId}";

        public static string BuildRoute(Guid QuizId)
            => Route.Replace("{QuizId}", QuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetQuiz))
                .WithTags("Quizzes")
                .WithSummary("Gets Quiz by its ID");
        }

        private async Task<Results<Ok<QuizDto>, NotFound>> HandleAsync(
            Guid quizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new GetQuizQuery(quizId);

            var result = await mediator.Send(query, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.Ok(result.Value);
        }
    }
}
