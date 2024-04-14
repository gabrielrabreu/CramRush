using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.List
{
    public class ListStaticQuizzesHandler(IListStaticQuizzesService service) : IQueryHandler<ListStaticQuizzesQuery, Result<PagedList<StaticQuizBriefDto>>>
    {
        public async Task<Result<PagedList<StaticQuizBriefDto>>> Handle(ListStaticQuizzesQuery request, CancellationToken cancellationToken)
        {
            return await service.ListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
