using Cramming.Domain.TopicAggregate;
using System.ComponentModel.DataAnnotations;

namespace Cramming.API.Topics
{
    /// <summary>
    /// Represents a request to replace questions for a topic.
    /// </summary>
    public class ReplaceQuestionsRequest
    {
        /// <summary>
        /// The list of questions to replace for the topic.
        /// </summary>
        [Required]
        public required IEnumerable<CreateQuestion> Questions { get; set; }

        /// <summary>
        /// Represents a question to be created or replaced.
        /// </summary>
        public class CreateQuestion
        {
            /// <summary>
            /// The type of the question.
            /// </summary>
            [Required]
            public required QuestionType Type { get; set; }

            /// <summary>
            /// The statement of the question.
            /// </summary>
            [Required]
            public required string Statement { get; set; }

            /// <summary>
            /// The answer to the question, if applicable.
            /// </summary>
            public string? Answer { get; set; }

            /// <summary>
            /// The list of options for a multiple-choice question, if applicable.
            /// </summary>
            [Required]
            public required IEnumerable<CreateMultipleChoiceQuestionOption> Options { get; set; }
        }

        /// <summary>
        /// Represents an option for a multiple-choice question.
        /// </summary>
        public class CreateMultipleChoiceQuestionOption
        {
            /// <summary>
            /// The statement of the option.
            /// </summary>
            [Required]
            public required string Statement { get; set; }

            /// <summary>
            /// Indicates whether the option is the correct answer.
            /// </summary>
            [Required]
            public required bool IsAnswer { get; set; }
        }
    }
}
