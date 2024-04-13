namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to search for topics.
    /// </summary>
    public class SearchTopicRequest
    {
        /// <summary>
        /// The page number for pagination.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize { get; set; }
    }
}
