namespace Cramming.SharedKernel
{
    /// <summary>
    /// Represents a paged list containing items along with pagination information.
    /// </summary>
    /// <typeparam name="T">The type of items in the paged list.</typeparam>
    public class PagedList<T>(
        IReadOnlyCollection<T> items,
        int count,
        int pageNumber,
        int pageSize)
    {
        /// <summary>
        /// Gets the collection of items in the current page.
        /// </summary>
        public IReadOnlyCollection<T> Items { get; } = items;

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int PageNumber { get; } = pageNumber;

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

        /// <summary>
        /// Gets the total count of items across all pages.
        /// </summary>
        public int TotalCount { get; } = count;

        /// <summary>
        /// Indicates whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Indicates whether there is a next page.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
