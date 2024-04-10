using Cramming.API.Endpoints.Extensions;
using Cramming.Application.Topics.Commands;
using Cramming.Domain.ValueObjects;
using Cramming.Infrastructure.Resources;
using System.Text;

namespace Cramming.API.Endpoints
{
    public class OpenAI : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(this);

            group.MapGet(GetQuestionGenerationPrompt, "question-generation-prompt")
                .Produces(StatusCodes.Status200OK)
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Get Prompt for Generating Questions",
                    Description = "Returns a prompt with instructions on how to generate questions for a given topic."
                });
        }

        public IResult GetQuestionGenerationPrompt()
        {
            var prompt = new StringBuilder()
                .AppendLine(string.Format(MyResources.PromptTopicId, nameof(OverrideQuestionsCommand.TopicId)))
                .AppendLine(string.Format(MyResources.PromptQuestions, nameof(OverrideQuestionsCommand.Questions)))
                .AppendLine(MyResources.PromptForEachQuestion)
                .AppendLine(string.Format(MyResources.PromptQuestionType, nameof(AssociateQuestionParameters.Type)))
                .AppendLine(string.Format(MyResources.PromptQuestionStatement, nameof(AssociateQuestionParameters.Statement)))
                .AppendLine(string.Format(MyResources.PromptQuestionAnswer, nameof(AssociateQuestionParameters.Answer)))
                .AppendLine(string.Format(MyResources.PromptQuestionOptions, nameof(AssociateQuestionParameters.Options)))
                .AppendLine(MyResources.PromptForEachQuestionOption)
                .AppendLine(string.Format(MyResources.PromptQuestionOptionStatement, nameof(AssociateMultipleChoiceQuestionOptionParameters.Statement)))
                .AppendLine(string.Format(MyResources.PromptQuestionOptionIsAnswer, nameof(AssociateMultipleChoiceQuestionOptionParameters.IsAnswer)))
                .ToString();

            return Results.Ok(prompt);
        }
    }
}
