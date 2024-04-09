using Cramming.Domain.ValueObjects;

namespace Cramming.Domain.Entities
{
    public class TopicOpenEndedQuestionEntity(Guid id, Guid topicId, string statement, string answer) : TopicQuestionEntity(id, topicId, statement)
    {
        public string Answer { get; set; } = answer;

        public TopicOpenEndedQuestionEntity(Guid topicId, AssociateQuestionParameters parameters) : this(Guid.NewGuid(), topicId, parameters.Statement, parameters.Answer)
        {
        }
    }
}
