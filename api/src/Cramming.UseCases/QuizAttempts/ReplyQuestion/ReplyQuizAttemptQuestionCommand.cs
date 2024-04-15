using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.AnswerQuestion
{
    public record ReplyQuizAttemptQuestionCommand(
        Guid QuizAttemptId,
        Guid QuestionId,
        Guid SelectedOptionId)
        : ICommand<Result<ReplyQuizAttemptQuestionResultDto>>;
}
