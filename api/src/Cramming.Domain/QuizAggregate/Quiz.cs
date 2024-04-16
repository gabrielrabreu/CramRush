namespace Cramming.Domain.QuizAggregate
{
    public class Quiz(string title) : DomainEntityBase, IAggregateRoot
    {
        public string Title { get; private set; } = title;

        public virtual ICollection<QuizQuestion> Questions { get; private set; } = [];

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void AddQuestion(QuizQuestion question)
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
