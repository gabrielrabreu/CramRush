namespace Cramming.UseCases.Topics
{
    public record TopicDto(Guid Id, string Name, IEnumerable<TagDto> Tags, IEnumerable<QuestionDto> Questions);
}
