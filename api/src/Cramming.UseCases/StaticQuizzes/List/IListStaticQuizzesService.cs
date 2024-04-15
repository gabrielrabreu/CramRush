using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.List
{
    public interface IListStaticQuizzesService
    {
        Task<PagedList<StaticQuizBriefDto>> ListAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
