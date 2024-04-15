using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Get
{
    public record GetStaticQuizQuery(Guid StaticQuizId) : IQuery<Result<StaticQuizDto>>
    {
    }
}
