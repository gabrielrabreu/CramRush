using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to create a new topic.
    /// </summary>
    public class CreateTopicRequest
    {
        /// <summary>
        /// The name of the topic to be created.
        /// </summary>
        [Required]
        public required string Name { get; set; }
    }
}
