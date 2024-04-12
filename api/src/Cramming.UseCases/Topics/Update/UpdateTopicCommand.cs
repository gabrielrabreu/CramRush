using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Update
{
    public record UpdateTopicCommand(Guid TopicId, string Name) : ICommand<Result>
    {
    }
}
