namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to retrieve notecards for a specific topic.
    /// </summary>
    public static class GetPracticeTestRequest
    {
        /// <summary>
        /// The route for retrieving notecards for a specific topic.
        /// </summary>
        public const string Route = "/Topics/{TopicId}:PracticeTest";

        /// <summary>
        /// Builds the route for retrieving notecards for the specified topic.
        /// </summary>
        /// <param name="topicId">The ID of the topic for which notecards will be retrieved.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());
    }
}
