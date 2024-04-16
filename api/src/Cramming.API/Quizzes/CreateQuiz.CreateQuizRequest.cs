using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Quizzes
{
    public class CreateQuizRequest
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required IEnumerable<CreateQuizQuestionRequest> Questions { get; set; }
    }
}
