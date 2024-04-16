using Cramming.UseCases.Quizzes.Delete;

namespace Cramming.API.Quizzes
{
    public class DeleteQuiz : EndpointBase
    {
        public const string Route = "/Quizzes/{QuizId}";

        public static string BuildRoute(Guid QuizId)
            => Route.Replace("{QuizId}", QuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapDelete(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DeleteQuiz))
                .WithTags("Quizzes")
                .WithSummary("Deletes Quiz by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            Guid quizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new DeleteQuizCommand(quizId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
