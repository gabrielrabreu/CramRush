using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Delete
{
    public record DeleteTopicCommand(Guid TopicId) : ICommand<Result>
    {
    }
}
