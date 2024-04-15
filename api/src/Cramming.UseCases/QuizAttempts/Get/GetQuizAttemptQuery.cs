using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.Get
{
    public record GetQuizAttemptQuery(Guid QuizAttemptId) : IQuery<Result<QuizAttemptDto>>
    {
    }
}
