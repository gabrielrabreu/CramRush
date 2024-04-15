using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.List
{
    public record ListStaticQuizzesQuery(int PageNumber, int PageSize) : IQuery<Result<PagedList<StaticQuizBriefDto>>>;
}
