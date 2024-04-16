namespace Cramming.UseCases.Quizzes.Get
{
    public record QuizDto(
        Guid Id,
        string Title,
        IEnumerable<QuizQuestionDto> Questions);

    public record QuizQuestionDto(
        Guid Id,
        string Statement,
        IEnumerable<QuizQuestionOptionDto> Options);

    public record QuizQuestionOptionDto(
        Guid Id,
        string Text,
        bool IsCorrect);
}
