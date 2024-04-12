using Cramming.Domain.TopicAggregate;

namespace Cramming.UseCases.Topics
{
    public record QuestionDto(Guid Id, Guid TopicId, QuestionType Type, string Statement, string? Answer, IEnumerable<MultipleChoiceQuestionOptionDto> Options);

}
