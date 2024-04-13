using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Search
{
    public class SearchTopicHandler(ISearchTopicQueryService service)
        : IQueryHandler<SearchTopicQuery, Result<PagedList<TopicBriefDto>>>
    {
        public async Task<Result<PagedList<TopicBriefDto>>> Handle(
            SearchTopicQuery request,
            CancellationToken cancellationToken)
        {
            return await service.SearchAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }
}
