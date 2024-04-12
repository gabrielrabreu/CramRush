using Cramming.SharedKernel;

namespace Cramming.Domain.TopicAggregate
{
    public class Tag(Guid topicId, string name) : DomainEntityBase
    {
        public Guid TopicId { get; private set; } = topicId;

        public virtual Topic? Topic { get; private set; }

        public string Name { get; private set; } = name;

        public Colour? Colour { get; private set; }

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void SetColour(Colour newColour)
        {
            Colour = newColour;
        }
    }
}
