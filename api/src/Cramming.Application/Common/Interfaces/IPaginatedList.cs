namespace Cramming.Application.Common.Interfaces
{
    public interface IPaginatedList<T>
    {
        IReadOnlyCollection<T> Items { get; }

        int PageNumber { get; }

        int TotalPages { get; }

        int TotalCount { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}
