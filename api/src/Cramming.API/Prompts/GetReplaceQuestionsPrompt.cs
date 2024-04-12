using Cramming.API.Topics;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cramming.API.Prompts
{
    public class GetReplaceQuestionsPrompt : EndpointBase
    {
        public override void Configure(WebApplication app)
        {
            app.MapPost(GetReplaceQuestionsPromptRequest.Route, Handle)
                .WithOpenApi()
                .WithName(nameof(GetReplaceQuestionsPrompt))
                .WithTags("Prompts")
                .WithSummary("Get Prompt for Replace Questions");
        }

        private Ok<GetReplaceQuestionsPromptResponse> Handle(
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var prompt = @"
1. List of Questions (questions):
   - For each question:
     - Type (type): Specify the type of the question, which can be ""OpenEnded"" or ""MultipleChoice"".
     - Statement (statement): Enter the statement of the question (string format).
     - Answer (answer): Enter the answer for the question (string format). For multiple-choice questions, leave it blank (null).
     - Options (options, only for multiple-choice questions):
       - For each options:
         - Statement (statement): Enter the statement of the options (string format).
         - Is answer? (isAnswer): Indicate if this is the correct answer (true / false).";

            var request = new GetReplaceQuestionsPromptResponse(prompt, ReplaceQuestionsRequest.Route);

            return TypedResults.Ok(request);
        }
    }
}
