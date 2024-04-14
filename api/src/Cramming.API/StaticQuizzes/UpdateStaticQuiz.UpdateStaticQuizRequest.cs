using System.ComponentModel.DataAnnotations;

namespace Cramming.API.StaticQuizzes
{
    public class UpdateStaticQuizRequest
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required IEnumerable<UpdateStaticQuizQuestionRequest> Questions { get; set; }
    }
}
