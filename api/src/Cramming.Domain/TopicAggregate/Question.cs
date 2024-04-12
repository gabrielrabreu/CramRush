using Cramming.SharedKernel;

namespace Cramming.Domain.TopicAggregate
{
    public abstract class Question(Guid topicId, string statement) : DomainEntityBase
    {
        public Guid TopicId { get; private set; } = topicId;

        public virtual Topic? Topic { get; private set; }

        public string Statement { get; private set; } = statement;
    }
}
