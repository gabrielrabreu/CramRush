using Cramming.Domain.ValueObjects;

namespace Cramming.Domain.Entities
{
    public class TopicMultipleChoiceQuestionEntity(Guid id, Guid topicId, string statement, List<TopicMultipleChoiceQuestionOptionEntity> options) : TopicQuestionEntity(id, topicId, statement)
    {
        private readonly List<TopicMultipleChoiceQuestionOptionEntity> _options = options;
        public IReadOnlyCollection<TopicMultipleChoiceQuestionOptionEntity> Options => _options;

        public TopicMultipleChoiceQuestionEntity(Guid topicId, AssociateQuestionParameters parameters) : this(Guid.NewGuid(), topicId, parameters.Statement, [])
        {
            foreach (var choice in parameters.Choices)
                AssociateOption(choice.Statement, choice.IsAnswer);
        }

        public TopicMultipleChoiceQuestionOptionEntity AssociateOption(string statement, bool isAnswer = false)
        {
            var newOption = new TopicMultipleChoiceQuestionOptionEntity(Id, statement, isAnswer);

            _options.Add(newOption);

            return newOption;
        }
    }
}
