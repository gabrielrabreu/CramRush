using Cramming.SharedKernel;

namespace Cramming.Domain.StaticQuizAggregate
{
    public class StaticQuizQuestion(string statement) : DomainEntityBase
    {
        public Guid QuizId { get; private set; } = Guid.Empty;

        public virtual StaticQuiz? Quiz { get; private set; }

        public string Statement { get; private set; } = statement;

        public virtual ICollection<StaticQuizQuestionOption> Options { get; private set; } = [];

        public void SetQuizId(Guid quizId)
        {
            QuizId = quizId;
        }

        public void AddOption(StaticQuizQuestionOption option)
        {
            option.SetQuestionId(Id);
            Options.Add(option);
        }
    }
}
