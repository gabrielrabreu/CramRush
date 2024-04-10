using Cramming.Infrastructure.Data.Common;

namespace Cramming.Infrastructure.Data.Entities
{
    public class TopicMultipleChoiceQuestionOptionData : DataAuditableEntity
    {
        public required Guid QuestionId { get; set; }

        public virtual TopicMultipleChoiceQuestionData? Question { get; set; }

        public required string Statement { get; set; }

        public required bool IsAnswer { get; set; }
    }
}
