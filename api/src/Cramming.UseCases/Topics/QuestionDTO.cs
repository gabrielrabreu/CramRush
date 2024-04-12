using Cramming.Domain.TopicAggregate;

namespace Cramming.UseCases.Topics
{
    public record QuestionDTO(Guid Id, Guid TopicId, QuestionType Type, string Statement, string? Answer, IEnumerable<MultipleChoiceQuestionOptionDTO> Options);

}
