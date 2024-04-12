using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Create
{
    public record CreateTopicCommand(string Name) : ICommand<Result<TopicBriefDTO>>
    {
    }
}
