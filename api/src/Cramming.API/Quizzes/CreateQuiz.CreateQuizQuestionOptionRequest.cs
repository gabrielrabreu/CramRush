using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Quizzes
{
    public class CreateQuizQuestionOptionRequest
    {
        [Required]
        public required string Text { get; set; }

        [Required]
        public required bool IsCorrect { get; set; }
    }
}
