namespace Cramming.UseCases.StaticQuizzes.Get
{
    public record StaticQuizDto(Guid Id, string Title, IEnumerable<StaticQuizQuestionDto> Questions);

    public record StaticQuizQuestionDto(Guid Id, string Statement, IEnumerable<StaticQuizQuestionOptionDto> Options);

    public record StaticQuizQuestionOptionDto(Guid Id, string Text, bool IsCorrect);
}
