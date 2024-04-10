namespace Cramming.Domain.Entities
{
    public abstract class TopicQuestionEntity(Guid id, Guid topicId, string statement)
    {
        public Guid Id { get; set; } = id;

        public string Statement { get; set; } = statement;

        public Guid TopicId { get; set; } = topicId;

        protected TopicQuestionEntity(Guid topicId, string name) : this(Guid.NewGuid(), topicId, name)
        {
        }
    }
}
