namespace Cramming.Domain.QuizAggregate
{
    public class QuizQuestion(string statement) : DomainEntityBase
    {
        public Guid QuizId { get; private set; } = Guid.Empty;

        public virtual Quiz? Quiz { get; private set; }

        public string Statement { get; private set; } = statement;

        public virtual ICollection<QuizQuestionOption> Options { get; private set; } = [];

        public void SetQuizId(Guid quizId)
        {
            QuizId = quizId;
        }

        public void AddOption(QuizQuestionOption option)
        {
            option.SetQuestionId(Id);
            Options.Add(option);
        }
    }
}
