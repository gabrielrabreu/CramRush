using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Get
{
    public record GetTopicQuery(Guid TopicId)
        : IQuery<Result<TopicDto>>
    {
    }
}
