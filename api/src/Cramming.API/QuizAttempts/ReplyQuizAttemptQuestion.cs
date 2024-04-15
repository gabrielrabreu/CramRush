using Cramming.UseCases.QuizAttempts.AnswerQuestion;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cramming.API.QuizAttempts
{
    public class ReplyQuizAttemptQuestion : EndpointBase
    {
        public const string Route = "/QuizAttempts/{QuizAttemptId}";

        public static string BuildRoute(Guid quizAttemptId) => Route.Replace("{QuizAttemptId}", quizAttemptId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapPut(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(ReplyQuizAttemptQuestion))
                .WithTags("QuizAttempts")
                .WithSummary("Updates Quiz Attempt by its ID");
        }

        private async Task<Results<Ok<ReplyQuizAttemptQuestionResultDto>, BadRequest, NotFound>> HandleAsync(
            [FromBody] ReplyQuizAttemptQuestionRequest request,
            Guid quizAttemptId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new ReplyQuizAttemptQuestionCommand(quizAttemptId, request.QuestionId, request.SelectedOptionId);

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            if (result.Status == HttpStatusCode.BadRequest)
                return TypedResults.BadRequest();

            return TypedResults.Ok(result.Value);
        }
    }
}
