using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.UseCases.QuizAttempts
{
    public interface IQuizAttemptRepository
        : IQuizAttemptReadRepository, IRepository<QuizAttempt>;
}
