using Cramming.SharedKernel;

namespace Cramming.Domain.QuizAttemptAggregate
{
    public class QuizAttemptQuestionOption(string text, bool isCorrect) : DomainEntityBase
    {
        public Guid QuestionId { get; private set; } = Guid.Empty;

        public virtual QuizAttemptQuestion? Question { get; private set; }

        public string Text { get; private set; } = text;

        public bool IsCorrect { get; private set; } = isCorrect;

        public bool IsSelected { get; private set; } = false;

        public void SetQuestionId(Guid questionId)
        {
            QuestionId = questionId;
        }

        public void MarkAsSelected()
        {
            IsSelected = true;
        }
    }
}
