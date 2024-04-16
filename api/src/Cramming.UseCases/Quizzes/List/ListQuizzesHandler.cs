namespace Cramming.UseCases.Quizzes.List
{
    public class ListQuizzesHandler(
        IListQuizzesService service)
        : IQueryHandler<ListQuizzesQuery, Result<PagedList<QuizBriefDto>>>
    {
        public async Task<Result<PagedList<QuizBriefDto>>> Handle(
            ListQuizzesQuery request,
            CancellationToken cancellationToken)
        {
            return await service.ListAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }
}
