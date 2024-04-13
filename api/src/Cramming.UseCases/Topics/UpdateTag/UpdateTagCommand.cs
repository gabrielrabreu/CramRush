using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.UpdateTag
{
    public record UpdateTagCommand(Guid Id, Guid TopicId, string Name, string? Colour) : ICommand<Result>
    {
    }
}
