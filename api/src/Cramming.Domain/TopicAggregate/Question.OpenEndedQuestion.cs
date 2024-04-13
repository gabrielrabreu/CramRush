namespace Cramming.Domain.TopicAggregate
{
    public class OpenEndedQuestion(Guid topicId, string statement, string answer) : Question(topicId, statement)
    {
        public string Answer { get; set; } = answer;
    }
}
