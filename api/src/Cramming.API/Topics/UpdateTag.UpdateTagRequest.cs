using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to update a tag associated with a topic.
    /// </summary>
    public class UpdateTagRequest
    {
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
