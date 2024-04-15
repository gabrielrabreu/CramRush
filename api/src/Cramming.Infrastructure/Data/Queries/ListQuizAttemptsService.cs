using Cramming.SharedKernel;
using Cramming.UseCases.QuizAttempts.List;

namespace Cramming.Infrastructure.Data.Queries
{
    public class ListQuizAttemptsService(AppDbContext db) : IListQuizAttemptsService
    {
        public async Task<PagedList<QuizAttemptBriefDto>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await db.QuizAttempts
                .OrderBy(attempt => attempt.QuizTitle)
                .Select(attempt => new QuizAttemptBriefDto(attempt.Id, attempt.QuizTitle, attempt.IsPending))
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}
