using System.ComponentModel.DataAnnotations;

namespace Cramming.API.StaticQuizzes
{
    public class UpdateStaticQuizQuestionRequest
    {
        [Required]
        public required string Statement { get; set; }

        [Required]
        public required IEnumerable<UpdateStaticQuizQuestionOptionRequest> Options { get; set; }
    }
}
