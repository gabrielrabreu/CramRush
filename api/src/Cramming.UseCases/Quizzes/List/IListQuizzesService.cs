namespace Cramming.UseCases.Quizzes.List
{
    public interface IListQuizzesService
    {
        Task<PagedList<QuizBriefDto>> ListAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
