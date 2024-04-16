namespace Cramming.UseCases.Quizzes.Create
{
    public record CreateQuizCommand(
        string Title,
        IEnumerable<CreateQuizCommand.QuestionDto> Questions) : ICommand<Result<QuizBriefDto>>
    {
        public record QuestionDto(string Statement, IEnumerable<QuestionOptionDto> Options);

        public record QuestionOptionDto(string Text, bool IsCorrect);
    }
}
