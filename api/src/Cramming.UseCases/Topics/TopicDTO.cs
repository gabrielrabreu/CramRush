namespace Cramming.UseCases.Topics
{
    public record TopicDTO(Guid Id, string Name, IEnumerable<TagDTO> Tags, IEnumerable<QuestionDTO> Questions);
}
