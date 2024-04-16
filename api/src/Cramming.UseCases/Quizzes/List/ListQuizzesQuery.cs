namespace Cramming.UseCases.Quizzes.List
{
    public record ListQuizzesQuery(
        int PageNumber,
        int PageSize)
        : IQuery<Result<PagedList<QuizBriefDto>>>;
}
