using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to create a new tag for a topic.
    /// </summary>
    public class CreateTagRequest
    {
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
