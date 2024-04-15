using Cramming.UseCases.QuizAttempts.DownloadToReply;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.QuizAttempts
{
    public class DownloadQuizAttemptToReply : EndpointBase
    {
        public const string Route = "/QuizAttempts/{QuizAttemptId}:Download";

        public static string BuildRoute(Guid quizAttemptId) => Route.Replace("{QuizAttemptId}", quizAttemptId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(DownloadQuizAttemptToReply))
                .WithTags("QuizAttempts")
                .WithSummary("Downloads Quiz Attempt to reply by its ID");
        }

        private async Task<Results<FileContentHttpResult, NotFound>> HandleAsync(
            Guid quizAttemptId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new DownloadQuizAttemptToReplyQuery(quizAttemptId);

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
