using Cramming.UseCases.Quizzes.DownloadFlashcards;

namespace Cramming.API.Quizzes
{
    public class DownloadQuizFlashcards : EndpointBase
    {
        public const string Route = "/Quizzes/{QuizId}:Download";

        public static string BuildRoute(Guid QuizId)
            => Route.Replace("{QuizId}", QuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DownloadQuizFlashcards))
                .WithTags("Quizzes")
                .WithSummary("Downloads Quiz flashcards by its ID");
        }

        private async Task<Results<FileContentHttpResult, NotFound>> HandleAsync(
            Guid quizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new DownloadQuizFlashcardsQuery(quizId);

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
