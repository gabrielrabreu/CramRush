namespace Cramming.Domain.Entities
{
    public class TopicMultipleChoiceQuestionOptionEntity(Guid id, Guid questionId, string statement, bool isAnswer)
    {
        public Guid Id { get; set; } = id;

        public Guid QuestionId { get; set; } = questionId;

        public string Statement { get; set; } = statement;

        public bool IsAnswer { get; set; } = isAnswer;

        public TopicMultipleChoiceQuestionOptionEntity(Guid questionId, string statement, bool isAnswer) : this(Guid.NewGuid(), questionId, statement, isAnswer)
        {
        }
    }
}
