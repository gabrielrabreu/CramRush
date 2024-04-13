namespace Cramming.FunctionalTests.Support
{
    public record StaticPagedList<T>(
        IReadOnlyCollection<T> Items,
        int PageNumber,
        int TotalPages,
        int TotalCount,
        bool HasPreviousPage,
        bool HasNextPage);
}
