using Cramming.SharedKernel;

namespace Cramming.Domain.QuizAttemptAggregate
{
    public class QuizAttemptQuestion(string statement) : DomainEntityBase
    {
        public Guid QuizAttemptId { get; private set; } = Guid.Empty;

        public virtual QuizAttempt? QuizAttempt { get; private set; }

        public string Statement { get; private set; } = statement;

        public bool IsPending { get; private set; } = true;

        public bool IsCorrect { get; private set; } = false;

        public virtual ICollection<QuizAttemptQuestionOption> Options { get; private set; } = [];

        public void SetQuizAttemptId(Guid quizAttemptId)
        {
            QuizAttemptId = quizAttemptId;
        }

        public void AddOption(QuizAttemptQuestionOption option)
        {
            option.SetQuestionId(Id);
            Options.Add(option);
        }

        public void MarkAnswer(QuizAttemptQuestionOption selectedOption)
        {
            selectedOption.MarkAsSelected();

            var correctOption = Options.Single(option => option.IsCorrect);

            IsPending = false;
            IsCorrect = correctOption.Id == selectedOption.Id;
        }
    }
}
