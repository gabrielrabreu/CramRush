using Cramming.API.Quizzes;

namespace Cramming.API.Prompts
{
    public class GetQuizPrompt : EndpointBase
    {
        public const string Route = "/Prompts/Quizzes";

        public static string BuildRoute() => Route;

        public override void Configure(WebApplication app)
        {
            app.MapGet(Route, Handle)
                .WithOpenApi()
                .WithName(nameof(GetQuizPrompt))
                .WithTags("Prompts")
                .WithSummary("Get Prompt for create Quiz");
        }

        private Ok<GetQuizPromptResponse> Handle()
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

            var request = new GetQuizPromptResponse(prompt, CreateQuiz.Route);

            return TypedResults.Ok(request);
        }
    }
}
