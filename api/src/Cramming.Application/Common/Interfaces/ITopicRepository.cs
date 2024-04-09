using Cramming.Domain.Entities;

namespace Cramming.Application.Common.Interfaces
{
    public interface ITopicRepository
    {
        Task<TopicEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<TopicEntity> AddAsync(TopicEntity domain, CancellationToken cancellationToken);
        Task UpdateAsync(TopicEntity domain, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
