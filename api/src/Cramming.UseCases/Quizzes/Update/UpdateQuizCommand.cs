namespace Cramming.UseCases.Quizzes.Update
{
    public record UpdateQuizCommand(
        Guid QuizId,
        string Title,
        IEnumerable<UpdateQuizCommand.QuestionDto> Questions) : ICommand<Result>
    {
        public record QuestionDto(
            string Statement,
            IEnumerable<QuestionOptionDto> Options);

        public record QuestionOptionDto(
            string Text,
            bool IsCorrect);
    }
}
