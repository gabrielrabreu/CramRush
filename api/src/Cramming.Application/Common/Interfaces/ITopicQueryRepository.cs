using Cramming.Application.Queries;

namespace Cramming.Application.Common.Interfaces
{
    public interface ITopicQueryRepository
    {
        Task<TopicDetailDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken);

        Task<IPaginatedList<TopicBriefDto>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
