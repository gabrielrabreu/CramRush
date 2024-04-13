using Cramming.SharedKernel;

namespace Cramming.Domain.TopicAggregate.Repositories
{

    public interface ITopicRepository : ITopicReadRepository, IRepository<Topic>
    {
    }
}
