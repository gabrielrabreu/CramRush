using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Quizzes
{
    public class UpdateQuizQuestionOptionRequest
    {
        [Required]
        public required string Text { get; set; }

        [Required]
        public required bool IsCorrect { get; set; }
    }
}
