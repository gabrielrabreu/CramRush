namespace Cramming.UseCases.QuizAttempts.List
{
    public record QuizAttemptBriefDto(
        Guid Id,
        string QuizTitle,
        bool IsPending);
}
