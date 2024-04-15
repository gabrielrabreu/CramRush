using Cramming.UseCases.StaticQuizzes.Update;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cramming.API.StaticQuizzes
{
    public class UpdateStaticQuiz : EndpointBase
    {
        public const string Route = "/StaticQuizzes/{StaticQuizId}";

        public static string BuildRoute(Guid staticQuizId) => Route.Replace("{StaticQuizId}", staticQuizId.ToString());

        public override void Configure(WebApplication app)
        {
            app.MapPut(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(UpdateStaticQuiz))
                .WithTags("StaticQuizzes")
                .WithSummary("Updates Static Quiz by its ID");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            [FromBody] UpdateStaticQuizRequest request,
            Guid staticQuizId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new UpdateStaticQuizCommand(
                staticQuizId,
                request.Title,
                request.Questions.Select(
                    question =>
                        new UpdateStaticQuizCommand.QuestionDto(
                            question.Statement,
                            question.Options.Select(
                                option => new UpdateStaticQuizCommand.QuestionOptionDto(option.Text, option.IsCorrect)))));

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
