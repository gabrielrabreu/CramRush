using Cramming.Infrastructure.Data.Common;

namespace Cramming.Infrastructure.Data.Entities
{
    public class TopicTagData : DataAuditableEntity
    {
        public required Guid TopicId { get; set; }

        public virtual TopicData? Topic { get; set; }

        public required string Name { get; set; }
    }
}
