using Cramming.API.StaticQuizzes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cramming.API.Prompts
{
    public class GetStaticQuizPrompt : EndpointBase
    {
        public const string Route = "/Prompts/StaticQuizzes";

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, Handle)
                .WithOpenApi()
                .WithName(nameof(GetStaticQuizPrompt))
                .WithTags("Prompts")
                .WithSummary("Get Prompt for create StaticQuiz");
        }

        private Ok<GetStaticQuizPromptResponse> Handle()
        {
            var prompt = @"
1. Title of the Quiz (title)
1. List of Questions (questions):
   - For each question:
     - Statement (statement): Enter the statement of the question (string format).
     - Options (options):
       - For each options:
         - Text (text): Enter the text of the option (string format).
         - Is correct? (isCorrect): Indicate if this is the correct answer (true / false).";

            var request = new GetStaticQuizPromptResponse(prompt, CreateStaticQuiz.Route);

            return TypedResults.Ok(request);
        }
    }
}
