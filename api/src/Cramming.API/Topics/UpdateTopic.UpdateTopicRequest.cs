using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to update a topic.
    /// </summary>
    public class UpdateTopicRequest
    {
        /// <summary>
        /// The new name for the topic.
        /// </summary>
        [Required]
        public required string Name { get; set; }
    }
}
