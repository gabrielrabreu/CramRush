namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to delete a topic.
    /// </summary>
    public static class DeleteTopicRequest
    {
        /// <summary>
        /// The route for deleting a topic with a specified ID.
        /// </summary>
        public const string Route = "/Topics/{TopicId}";

        /// <summary>
        /// Builds the route for deleting a topic with the provided ID.
        /// </summary>
        /// <param name="topicId">The ID of the topic to be deleted.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());
    }
}
