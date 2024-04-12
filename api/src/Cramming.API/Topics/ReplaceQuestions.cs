using Cramming.UseCases.Topics.ReplaceQuestions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cramming.API.Topics
{
    public class ReplaceQuestions : EndpointBase
    {
        public override void Configure(WebApplication app)
        {
            app.MapPost(ReplaceQuestionsRequest.Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(ReplaceQuestions))
                .WithTags("Topics")
                .WithSummary("Replace questions for a topic");
        }

        private async Task<Results<NoContent, NotFound>> HandleAsync(
            [FromBody] ReplaceQuestionsRequest request,
            Guid topicId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var command = new ReplaceQuestionsCommand(
                topicId,
                request.Questions.Select(
                    question =>
                        new ReplaceQuestionsCommand.CreateQuestion(
                            question.Type,
                            question.Statement,
                            question.Answer,
                            question.Options.Select(
                                option =>
                                    new ReplaceQuestionsCommand.CreateMultipleChoiceQuestionOption(
                                        option.Statement,
                                        option.IsAnswer)))));

            var result = await mediator.Send(command, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
