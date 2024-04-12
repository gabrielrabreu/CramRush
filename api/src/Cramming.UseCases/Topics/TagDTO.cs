namespace Cramming.UseCases.Topics
{
    public record TagDTO(Guid Id, Guid TopicId, string Name, string? Colour);
}
