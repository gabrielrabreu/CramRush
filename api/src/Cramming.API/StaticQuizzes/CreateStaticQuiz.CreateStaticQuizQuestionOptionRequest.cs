using System.ComponentModel.DataAnnotations;

namespace Cramming.API.StaticQuizzes
{
    public class CreateStaticQuizQuestionOptionRequest
    {
        [Required]
        public required string Text { get; set; }

        [Required]
        public required bool IsCorrect { get; set; }
    }
}
