using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.List
{
    public interface IListQuizAttemptsService
    {
        Task<PagedList<QuizAttemptBriefDto>> ListAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
