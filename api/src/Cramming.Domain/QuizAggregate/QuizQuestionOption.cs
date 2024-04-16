namespace Cramming.Domain.QuizAggregate
{
    public class QuizQuestionOption(string text, bool isCorrect) : DomainEntityBase
    {
        public Guid QuestionId { get; private set; } = Guid.Empty;

        public virtual QuizQuestion? Question { get; private set; }

        public string Text { get; private set; } = text;

        public bool IsCorrect { get; private set; } = isCorrect;

        public void SetQuestionId(Guid questionId)
        {
            QuestionId = questionId;
        }

    }
}
