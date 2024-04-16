namespace Cramming.Domain.QuizAttemptAggregate
{
    public class QuizAttempt(string quizTitle) : DomainEntityBase, IAggregateRoot
    {
        public string QuizTitle { get; private set; } = quizTitle;

        public bool IsPending { get; private set; } = true;

        public virtual ICollection<QuizAttemptQuestion> Questions { get; private set; } = [];

        public void AddQuestion(QuizAttemptQuestion question)
        {
            question.SetQuizAttemptId(Id);
            Questions.Add(question);
        }

        public void MarkAnswer(QuizAttemptQuestion question, QuizAttemptQuestionOption option)
        {
            question.MarkAnswer(option);

            IsPending = Questions.Any(question => question.IsPending);
        }
    }
}
