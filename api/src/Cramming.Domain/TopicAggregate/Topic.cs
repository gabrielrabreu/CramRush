using Cramming.SharedKernel;

namespace Cramming.Domain.TopicAggregate
{
    public class Topic(string name) : DomainEntityBase, IAggregateRoot
    {
        public string Name { get; private set; } = name;

        public virtual ICollection<Tag> Tags { get; private set; } = [];

        public virtual ICollection<Question> Questions { get; private set; } = [];

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public Tag AddTag(string tagName, string? tagColour)
        {
            var tag = new Tag(Id, tagName);
            tag.SetColour(new Colour(tagColour));
            Tags.Add(tag);
            return tag;
        }

        public bool HasTag(Guid tagId)
        {
            return Tags.Any(tag => tag.Id == tagId);
        }

        public bool DoesNotHaveTag(Guid tagId)
        {
            return !HasTag(tagId);
        }

        public void UpdateTagName(Guid tagId, string tagName)
        {
            var tag = Tags.SingleOrDefault(tag => tag.Id == tagId);
            tag?.UpdateName(tagName);
        }

        public void UpdateTagColour(Guid tagId, string? tagColour)
        {
            var tag = Tags.SingleOrDefault(tag => tag.Id == tagId);
            tag?.SetColour(new Colour(tagColour));
        }

        public void DeleteTag(Guid tagId)
        {
            var tag = Tags.SingleOrDefault(tag => tag.Id == tagId);
            if (tag != null)
                Tags.Remove(tag);
        }

        public void ClearQuestions()
        {
            Questions.Clear();
        }

        public OpenEndedQuestion AddOpenEndedQuestion(string questionStatement, string questionAnswer)
        {
            var question = new OpenEndedQuestion(Id, questionStatement, questionAnswer);
            Questions.Add(question);
            return question;
        }

        public MultipleChoiceQuestion AddMultipleChoiceQuestion(string questionStatement)
        {
            var question = new MultipleChoiceQuestion(Id, questionStatement);
            Questions.Add(question);
            return question;
        }

        public bool HasTags()
        {
            return Tags.Count > 0;
        }
    }
}
