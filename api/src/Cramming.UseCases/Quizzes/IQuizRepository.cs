using Cramming.Domain.QuizAggregate;

namespace Cramming.UseCases.Quizzes
{
    public interface IQuizRepository
        : IQuizReadRepository, IRepository<Quiz>;
}
