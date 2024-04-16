using System.ComponentModel.DataAnnotations;

namespace Cramming.API.QuizAttempts
{
    public class CreateQuizAttemptRequest
    {
        [Required]
        public required Guid QuizId { get; set; }
    }
}
