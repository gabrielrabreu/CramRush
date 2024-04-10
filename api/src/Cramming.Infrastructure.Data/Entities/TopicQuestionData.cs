using Cramming.Infrastructure.Data.Common;

namespace Cramming.Infrastructure.Data.Entities
{
    public abstract class TopicQuestionData : DataAuditableEntity
    {
        public required Guid TopicId { get; set; }

        public virtual TopicData? Topic { get; set; }

        public required string Statement { get; set; }
    }
}
