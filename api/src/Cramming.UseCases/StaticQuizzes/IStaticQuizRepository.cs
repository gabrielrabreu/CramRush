using Cramming.Domain.StaticQuizAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes
{
    public interface IStaticQuizReadRepository : IReadRepository<StaticQuiz>
    {
    }

    public interface IStaticQuizRepository : IStaticQuizReadRepository, IRepository<StaticQuiz>
    {
    }
}
