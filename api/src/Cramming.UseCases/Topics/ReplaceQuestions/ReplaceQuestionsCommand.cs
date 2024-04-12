using Cramming.Domain.TopicAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.ReplaceQuestions
{
    public record ReplaceQuestionsCommand(
        Guid TopicId,
        IEnumerable<ReplaceQuestionsCommand.CreateQuestion> Questions) : ICommand<Result>
    {
        public record CreateQuestion(QuestionType Type, string Statement, string? Answer, IEnumerable<CreateMultipleChoiceQuestionOption> Options);

        public record CreateMultipleChoiceQuestionOption(string Statement, bool IsAnswer);
    }
}
