namespace Cramming.UseCases.Topics
{
    public record MultipleChoiceQuestionOptionDTO(Guid Id, Guid QuestionId, string Statement, bool IsAnswer);
}
