namespace Cramming.UseCases.Topics
{
    public record TagDto(Guid Id, Guid TopicId, string Name, string? Colour);
}
