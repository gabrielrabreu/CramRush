using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.List;

namespace Cramming.Infrastructure.Data.Queries
{
    public class ListStaticQuizzesService(AppDbContext db) : IListStaticQuizzesService
    {
        public async Task<PagedList<StaticQuizBriefDto>> ListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await db.StaticQuizzes
                .OrderBy(quiz => quiz.Title)
                .Select(quiz => new StaticQuizBriefDto(quiz.Id, quiz.Title, quiz.Questions.Count))
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}
