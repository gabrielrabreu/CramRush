namespace Cramming.UseCases.Topics
{
    public record MultipleChoiceQuestionOptionDto(Guid Id, Guid QuestionId, string Statement, bool IsAnswer);
}
