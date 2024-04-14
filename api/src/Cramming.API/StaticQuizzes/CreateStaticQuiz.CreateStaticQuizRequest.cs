using System.ComponentModel.DataAnnotations;

namespace Cramming.API.StaticQuizzes
{
    public class CreateStaticQuizRequest
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required IEnumerable<CreateStaticQuizQuestionRequest> Questions { get; set; }
    }
}
