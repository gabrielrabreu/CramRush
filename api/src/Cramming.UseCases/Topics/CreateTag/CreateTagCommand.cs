using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.CreateTag
{
    public record CreateTagCommand(Guid TopicId, string Name, string? Colour) : ICommand<Result<TagDTO>>
    {
    }
}
