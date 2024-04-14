using Cramming.SharedKernel;

namespace Cramming.Domain.StaticQuizAggregate
{
    public class StaticQuiz(string title) : DomainEntityBase, IAggregateRoot
    {
        public string Title { get; private set; } = title;

        public virtual ICollection<StaticQuizQuestion> Questions { get; private set; } = [];

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void AddQuestion(StaticQuizQuestion question)
        {
            question.SetQuizId(Id);
            Questions.Add(question);
        }

        public void ClearQuestions()
        {
            Questions.Clear();
        }
    }
}
