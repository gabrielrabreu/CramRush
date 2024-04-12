using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to create a new tag for a topic.
    /// </summary>
    public class CreateTagRequest
    {
        /// <summary>
        /// The route for creating a new tag for a topic.
        /// </summary>
        public const string Route = "/Topics/{TopicId}/Tags";

        /// <summary>
        /// Builds the route for creating a new tag for the specified topic.
        /// </summary>
        /// <param name="topicId">The ID of the topic for which the tag is being created.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId) => Route.Replace("{TopicId}", topicId.ToString());

        /// <summary>
        /// The name of the tag to be created.
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// The color of the tag, if provided.
        /// </summary>
        public string? Colour { get; set; }
    }
}
