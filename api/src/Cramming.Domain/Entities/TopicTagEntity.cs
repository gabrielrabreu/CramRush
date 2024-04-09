namespace Cramming.Domain.Entities
{
    public class TopicTagEntity(Guid id, Guid topicId, string name)
    {
        public Guid Id { get; set; } = id;

        public string Name { get; set; } = name;

        public Guid TopicId { get; set; } = topicId;

        public TopicTagEntity(Guid topicId, string name) : this(Guid.NewGuid(), topicId, name)
        {
        }
    }
}
