using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Create;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Cramming.API.StaticQuizzes
{
    public class CreateStaticQuiz : EndpointBase
    {
        public const string Route = "/StaticQuizzes";

        public override void Configure(WebApplication app)
        {
            app.MapPost(Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(CreateStaticQuiz))
                .WithTags("StaticQuizzes")
                .WithSummary("Creates Static Quiz");
        }

        private async Task<Created<StaticQuizBriefDto>> HandleAsync(
            [FromBody] CreateStaticQuizRequest request,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new CreateStaticQuizCommand(
                request.Title,
                request.Questions.Select(
                    question =>
                        new CreateStaticQuizCommand.QuestionDto(
                            question.Statement,
                            question.Options.Select(
                                option => new CreateStaticQuizCommand.QuestionOptionDto(option.Text, option.IsCorrect)))));

            var result = await mediator.Send(command, cancellationToken);

            return TypedResults.Created(GetStaticQuiz.BuildRoute(result.Value!.Id), result.Value);
        }
    }
}
