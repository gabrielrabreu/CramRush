using Cramming.Infrastructure.Data.Common;

namespace Cramming.Infrastructure.Data.Entities
{
    public class TopicData : DataAuditableEntity
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required virtual ICollection<TopicTagData> Tags { get; set; }

        public required virtual ICollection<TopicQuestionData> Questions { get; set; }
    }
}
