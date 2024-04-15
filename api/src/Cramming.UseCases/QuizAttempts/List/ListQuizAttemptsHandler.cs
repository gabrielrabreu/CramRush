using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.List
{
    public class ListQuizAttemptsHandler(IListQuizAttemptsService service) : IQueryHandler<ListQuizAttemptsQuery, Result<PagedList<QuizAttemptBriefDto>>>
    {
        public async Task<Result<PagedList<QuizAttemptBriefDto>>> Handle(ListQuizAttemptsQuery request, CancellationToken cancellationToken)
        {
            return await service.ListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
