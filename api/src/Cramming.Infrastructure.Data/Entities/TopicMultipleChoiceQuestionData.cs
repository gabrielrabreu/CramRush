namespace Cramming.Infrastructure.Data.Entities
{
    public class TopicMultipleChoiceQuestionData : TopicQuestionData
    {
        public required virtual ICollection<TopicMultipleChoiceQuestionOptionData> Options { get; set; }
    }
}
