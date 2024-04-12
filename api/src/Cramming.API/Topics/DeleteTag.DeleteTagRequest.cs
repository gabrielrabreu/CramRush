namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to delete a tag associated with a topic.
    /// </summary>
    public class DeleteTagRequest
    {
        /// <summary>
        /// The route for deleting a tag associated with a topic.
        /// </summary>
        public const string Route = "/Topics/{TopicId}/Tags/{TagId}";

        /// <summary>
        /// Builds the route for deleting the specified tag associated with the topic.
        /// </summary>
        /// <param name="topicId">The ID of the topic from which the tag will be deleted.</param>
        /// <param name="tagId">The ID of the tag to be deleted.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId, Guid tagId)
            => Route
                   .Replace("{TopicId}", topicId.ToString())
                   .Replace("{TagId}", tagId.ToString());
    }
}
