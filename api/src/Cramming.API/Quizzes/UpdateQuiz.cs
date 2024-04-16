using Cramming.UseCases.Quizzes.Update;

namespace Cramming.API.Quizzes
{
    public class UpdateQuiz : EndpointBase
    {
        public const string Route = "/Quizzes/{QuizId}";

        public static string BuildRoute(Guid QuizId)
            => Route.Replace("{QuizId}", QuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapPut(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(UpdateQuiz))
                .WithTags("Quizzes")
                .WithSummary("Updates Quiz by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            [FromBody] UpdateQuizRequest request,
            Guid quizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new UpdateQuizCommand(
                quizId,
                request.Title,
                request.Questions.Select(
                    question =>
                        new UpdateQuizCommand.QuestionDto(
                            question.Statement,
                            question.Options.Select(
                                option => new UpdateQuizCommand.QuestionOptionDto(option.Text, option.IsCorrect)))));

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
