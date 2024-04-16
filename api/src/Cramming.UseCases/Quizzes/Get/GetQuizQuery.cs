namespace Cramming.UseCases.Quizzes.Get
{
    public record GetQuizQuery(
        Guid QuizId)
        : IQuery<Result<QuizDto>>
    {
    }
}
