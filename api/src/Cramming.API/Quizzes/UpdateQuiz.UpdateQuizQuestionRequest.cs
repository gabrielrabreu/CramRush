using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Quizzes
{
    public class UpdateQuizQuestionRequest
    {
        [Required]
        public required string Statement { get; set; }

        [Required]
        public required IEnumerable<UpdateQuizQuestionOptionRequest> Options { get; set; }
    }
}
