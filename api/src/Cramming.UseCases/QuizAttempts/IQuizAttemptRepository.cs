using Cramming.Domain.QuizAttemptAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes
{
    public interface IQuizAttemptRepository : IQuizAttemptReadRepository, IRepository<QuizAttempt>
    {
    }
}
