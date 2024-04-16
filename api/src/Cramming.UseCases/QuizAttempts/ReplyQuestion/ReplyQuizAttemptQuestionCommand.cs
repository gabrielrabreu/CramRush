namespace Cramming.UseCases.QuizAttempts.ReplyQuestion
{
    public record ReplyQuizAttemptQuestionCommand(
        Guid QuizAttemptId,
        Guid QuestionId,
        Guid SelectedOptionId)
        : ICommand<Result<ReplyQuizAttemptQuestionResultDto>>;
}
