using Cramming.Domain.Enums;

namespace Cramming.Domain.ValueObjects
{
    /// <summary>
    /// Represents the parameters for associating a question with a topic.
    /// </summary>
    public record AssociateQuestionParameters
    {
        /// <summary>
        /// The type of the question.
        /// </summary>
        public QuestionType Type { get; init; } = QuestionType.OpenEnded;

        /// <summary>
        /// The statement of the question.
        /// </summary>
        public string Statement { get; init; } = string.Empty;

        /// <summary>
        /// The answer to the question.
        /// </summary>
        public string Answer { get; init; } = string.Empty;

        /// <summary>
        /// The collection of parameters for associating multiple-choice options with the topic.
        /// </summary>
        public ICollection<AssociateMultipleChoiceQuestionOptionParameters> Options { get; init; } = [];
    }

    /// <summary>
    /// Represents the parameters for associating a multiple-choice question with a topic.
    /// </summary>
    public record AssociateMultipleChoiceQuestionOptionParameters
    {
        /// <summary>
        /// The statement of the multiple-choice option.
        /// </summary>
        public string Statement { get; init; } = string.Empty;

        /// <summary>
        /// Indicates whether the option is the correct answer.
        /// </summary>
        public bool IsAnswer { get; init; }
    }
}
