namespace Cramming.Domain.TopicAggregate
{
    public class MultipleChoiceQuestion(Guid topicId, string statement) : Question(topicId, statement)
    {
        public virtual ICollection<MultipleChoiceQuestionOption> Options { get; private set; } = [];

        public MultipleChoiceQuestionOption AddOption(string statement, bool isAnswer)
        {
            var newOption = new MultipleChoiceQuestionOption(Id, statement, isAnswer);

            Options.Add(newOption);

            return newOption;
        }
    }
}
