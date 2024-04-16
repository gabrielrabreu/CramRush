namespace Cramming.UseCases.QuizAttempts.Create
{
    public record CreateQuizAttemptCommand(
        Guid QuizId)
        : ICommand<Result<QuizAttemptDto>>;
}
