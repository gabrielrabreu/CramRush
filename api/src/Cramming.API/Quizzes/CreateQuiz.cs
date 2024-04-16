using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.Create;

namespace Cramming.API.Quizzes
{
    public class CreateQuiz : EndpointBase
    {
        public const string Route = "/Quizzes";

        public static string BuildRoute() => Route;

        public override void Configure(WebApplication app)
        {
            app.MapPost(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(CreateQuiz))
                .WithTags("Quizzes")
                .WithSummary("Creates Quiz");
        }

        private async Task<Created<QuizBriefDto>> HandleAsync(
            [FromBody] CreateQuizRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new CreateQuizCommand(
                request.Title,
                request.Questions.Select(
                    question =>
                        new CreateQuizCommand.QuestionDto(
                            question.Statement,
                            question.Options.Select(
                                option => new CreateQuizCommand.QuestionOptionDto(option.Text, option.IsCorrect)))));

            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Created(GetQuiz.BuildRoute(result.Value!.Id), result.Value);
        }
    }
}
