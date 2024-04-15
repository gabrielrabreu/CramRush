using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Update
{
    public record UpdateStaticQuizCommand(
        Guid StaticQuizId,
        string Title,
        IEnumerable<UpdateStaticQuizCommand.QuestionDto> Questions) : ICommand<Result>
    {
        public record QuestionDto(string Statement, IEnumerable<QuestionOptionDto> Options);

        public record QuestionOptionDto(string Text, bool IsCorrect);
    }
}
