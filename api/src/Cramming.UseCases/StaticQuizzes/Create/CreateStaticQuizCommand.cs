using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Create
{
    public record CreateStaticQuizCommand(
        string Title,
        IEnumerable<CreateStaticQuizCommand.QuestionDto> Questions) : ICommand<Result<StaticQuizBriefDto>>
    {
        public record QuestionDto(string Statement, IEnumerable<QuestionOptionDto> Options);

        public record QuestionOptionDto(string Text, bool IsCorrect);
    }
}
