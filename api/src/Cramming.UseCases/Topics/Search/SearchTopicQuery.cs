using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Search
{
    public record SearchTopicQuery(
        int PageNumber,
        int PageSize)
        : IQuery<Result<PagedList<TopicBriefDto>>>
    {
    }
}
