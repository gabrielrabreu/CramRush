using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.Create;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Cramming.API.QuizAttempts
{
    public class CreateQuizAttempt : EndpointBase
    {
        public const string Route = "/QuizAttempts";

        public override void Configure(WebApplication app)
        {
            app.MapPost(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(CreateQuizAttempt))
                .WithTags("QuizAttempts")
                .WithSummary("Creates Quiz Attempt");
        }

        private async Task<Created<QuizAttemptDto>> HandleAsync(
            [FromBody] CreateQuizAttemptRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new CreateQuizAttemptCommand(request.QuizId);

            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Created(GetQuizAttempt.BuildRoute(result.Value!.Id), result.Value);
        }
    }
}
