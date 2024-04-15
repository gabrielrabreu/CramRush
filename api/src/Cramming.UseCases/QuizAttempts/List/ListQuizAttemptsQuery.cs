using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.List
{
    public record ListQuizAttemptsQuery(int PageNumber, int PageSize) : IQuery<Result<PagedList<QuizAttemptBriefDto>>>;
}
