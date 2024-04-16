using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Quizzes
{
    public class UpdateQuizRequest
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required IEnumerable<UpdateQuizQuestionRequest> Questions { get; set; }
    }
}
