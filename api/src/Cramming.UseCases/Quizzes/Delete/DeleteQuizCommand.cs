namespace Cramming.UseCases.Quizzes.Delete
{
    public record DeleteQuizCommand(
        Guid QuizId)
        : ICommand<Result>;
}
