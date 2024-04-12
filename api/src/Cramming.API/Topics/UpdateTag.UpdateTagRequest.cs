using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to update a tag associated with a topic.
    /// </summary>
    public class UpdateTagRequest
    {
        /// <summary>
        /// The route for updating a tag associated with a topic.
        /// </summary>
        public const string Route = "/Topics/{TopicId}/Tags/{TagId}";

        /// <summary>
        /// Builds the route for updating the specified tag associated with the topic.
        /// </summary>
        /// <param name="topicId">The ID of the topic to which the tag belongs.</param>
        /// <param name="tagId">The ID of the tag to be updated.</param>
        /// <returns>The built route.</returns>
        public static string BuildRoute(Guid topicId, Guid tagId)
            => Route
                   .Replace("{TopicId}", topicId.ToString())
                   .Replace("{TagId}", tagId.ToString());

        /// <summary>
        /// The new name for the tag.
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// The new color for the tag, if provided.
        /// </summary>
        public string? Colour { get; set; }
    }
}
