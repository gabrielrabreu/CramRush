using Cramming.Domain.Common;
using Cramming.Domain.Enums;
using Cramming.Domain.ValueObjects;

namespace Cramming.Domain.Entities
{
    public class TopicEntity(Guid id, string name, string description, List<TopicTagEntity> tags, List<TopicQuestionEntity> questions) : BaseEntity
    {
        public Guid Id { get; set; } = id;

        public string Name { get; set; } = name;

        public string Description { get; set; } = description;

        private readonly List<TopicTagEntity> _tags = tags;
        public IReadOnlyCollection<TopicTagEntity> Tags => _tags;

        private readonly List<TopicQuestionEntity> _questions = questions;
        public IReadOnlyCollection<TopicQuestionEntity> Questions => _questions;

        public TopicEntity(string name, string description) : this(Guid.NewGuid(), name, description, [], [])
        {
        }

        public TopicTagEntity AssociateTag(string tagName, string? tagColour)
        {
            var newTag = new TopicTagEntity(Id, tagName, tagColour);

            _tags.Add(newTag);

            return newTag;
        }

        public void UpdateTag(Guid tagId, string tagName, string? tagColour)
        {
            var tag = _tags.SingleOrDefault(t => t.Id == tagId);

            if (tag != null)
            {
                tag.Name = tagName;
                tag.Colour = new Colour(tagColour);
            }
        }

        public void DisassociateTag(Guid tagId)
        {
            var tag = _tags.SingleOrDefault(p => p.Id == tagId);

            if (tag != null)
                _tags.Remove(tag);
        }

        public TopicQuestionEntity AssociateQuestion(AssociateQuestionParameters parameters)
        {
            TopicQuestionEntity newQuestion = parameters.Type switch
            {
                QuestionType.MultipleChoice => new TopicMultipleChoiceQuestionEntity(Id, parameters),
                _ => new TopicOpenEndedQuestionEntity(Id, parameters),
            };

            _questions.Add(newQuestion);

            return newQuestion;
        }

        public void ClearQuestions()
        {
            _questions.Clear();
        }
    }
}
