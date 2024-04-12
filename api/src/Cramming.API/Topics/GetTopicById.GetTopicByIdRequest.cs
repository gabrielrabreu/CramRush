namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to retrieve a topic by its ID.
    /// </summary>
    public static class GetTopicByIdRequest
    {
        /// <summary>
        /// The route for retrieving a topic with a specified ID.
        /// </summary>
        public const string Route = "/Topics/{TopicId}";

        /// <summary>
        /// Builds the route for retrieving a topic with the provided ID.
        /// </summary>
        /// <param name="topicId">The ID of the topic to be retrieved.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());
    }
}
