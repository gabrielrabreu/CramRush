using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to update a topic.
    /// </summary>
    public class UpdateTopicRequest
    {
        /// <summary>
        /// The route for updating a topic with a specified ID.
        /// </summary>
        public const string Route = "/Topics/{TopicId}";

        /// <summary>
        /// Builds the route for updating a topic with the provided ID.
        /// </summary>
        /// <param name="topicId">The ID of the topic to be updated.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());

        /// <summary>
        /// The new name for the topic.
        /// </summary>
        [Required]
        public required string Name { get; set; }
    }
}
