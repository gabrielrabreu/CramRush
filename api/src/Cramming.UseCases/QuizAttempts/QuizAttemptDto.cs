namespace Cramming.UseCases.QuizAttempts
{
    public record QuizAttemptDto(
        Guid Id,
        string QuizTitle,
        bool IsPending,
        IEnumerable<QuizAttemptQuestionDto> Questions);

    public record QuizAttemptQuestionDto(
        Guid Id,
        string Statement,
        bool IsPending,
        IEnumerable<QuizAttemptQuestionOptionDto> Options);

    public record QuizAttemptQuestionOptionDto(
        Guid Id,
        string Text,
        bool IsSelected);
}
