using Cramming.Domain.ValueObjects;

namespace Cramming.Domain.Entities
{
    public class TopicTagEntity(Guid id, Guid topicId, string name, string? colour)
    {
        public Guid Id { get; set; } = id;

        public Guid TopicId { get; set; } = topicId;

        public string Name { get; set; } = name;

        public Colour Colour { get; set; } = new Colour(colour);

        public TopicTagEntity(Guid topicId, string name, string? colour) : this(Guid.NewGuid(), topicId, name, colour)
        {
        }
    }
}
