using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.List;

namespace Cramming.Infrastructure.Data.Queries
{
    public class ListQuizzesService(
        AppDbContext db)
        : IListQuizzesService
    {
        public async Task<PagedList<QuizBriefDto>> ListAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await db.Quiz
                .OrderBy(quiz => quiz.Title)
                .Select(quiz =>
                    new QuizBriefDto(
                        quiz.Id,
                        quiz.Title,
                        quiz.Questions.Count))
                .ToPagedListAsync(
                    pageNumber,
                    pageSize,
                    cancellationToken);
        }
    }
}
