using System.ComponentModel.DataAnnotations;

namespace Cramming.API.QuizAttempts
{
    public class ReplyQuizAttemptQuestionRequest
    {
        [Required]
        public required Guid QuestionId { get; set; }

        [Required]
        public required Guid SelectedOptionId { get; set; }
    }
}
