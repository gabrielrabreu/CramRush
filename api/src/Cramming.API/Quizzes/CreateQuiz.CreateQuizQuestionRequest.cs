using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Quizzes
{
    public class CreateQuizQuestionRequest
    {
        [Required]
        public required string Statement { get; set; }

        [Required]
        public required IEnumerable<CreateQuizQuestionOptionRequest> Options { get; set; }
    }
}
