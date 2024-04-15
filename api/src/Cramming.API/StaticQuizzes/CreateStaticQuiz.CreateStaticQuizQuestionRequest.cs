using System.ComponentModel.DataAnnotations;

namespace Cramming.API.StaticQuizzes
{
    public class CreateStaticQuizQuestionRequest
    {
        [Required]
        public required string Statement { get; set; }

        [Required]
        public required IEnumerable<CreateStaticQuizQuestionOptionRequest> Options { get; set; }
    }
}
