using Cramming.Domain.Enums;

namespace Cramming.Domain.ValueObjects
{
    public record AssociateQuestionParameters
    {
        public QuestionType QuestionType { get; init; } = QuestionType.Undefined;

        public string Statement { get; init; } = string.Empty;

        public string Answer { get; init; } = string.Empty;

        public ICollection<AssociateMultipleChoiceQuestionParameters> Choices { get; init; } = [];
    }

    public record AssociateMultipleChoiceQuestionParameters
    {
        public string Statement { get; init; } = string.Empty;

        public bool IsAnswer { get; init; }
    }
}
