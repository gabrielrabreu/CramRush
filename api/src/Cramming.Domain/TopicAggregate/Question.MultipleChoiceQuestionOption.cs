using Cramming.SharedKernel;

namespace Cramming.Domain.TopicAggregate
{
    public class MultipleChoiceQuestionOption(Guid questionId, string statement, bool isAnswer) : DomainEntityBase
    {
        public Guid QuestionId { get; private set; } = questionId;

        public virtual MultipleChoiceQuestion? Question { get; private set; }

        public string Statement { get; private set; } = statement;

        public bool IsAnswer { get; private set; } = isAnswer;
    }
}
