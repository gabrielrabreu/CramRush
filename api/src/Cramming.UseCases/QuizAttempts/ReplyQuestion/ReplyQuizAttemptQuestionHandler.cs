using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.UseCases.QuizAttempts.AnswerQuestion
{
    public class ReplyQuizAttemptQuestionHandler(
        IQuizAttemptRepository quizAttemptRepository) 
        : ICommandHandler<ReplyQuizAttemptQuestionCommand, Result<ReplyQuizAttemptQuestionResultDto>>
    {
        public async Task<Result<ReplyQuizAttemptQuestionResultDto>> Handle(ReplyQuizAttemptQuestionCommand request, CancellationToken cancellationToken)
        {
            var attempt = await quizAttemptRepository.GetByIdAsync(request.QuizAttemptId, cancellationToken);
            if (attempt == null)
                return Result.NotFound();
            if (!attempt.IsPending)
                return Result.BadRequest();

            var question = attempt.Questions.SingleOrDefault(question => question.Id == request.QuestionId);
            if (question == null)
                return Result.NotFound();
            if (!question.IsPending)
                return Result.BadRequest();

            var selectedOption = question.Options.SingleOrDefault(option => option.Id == request.SelectedOptionId);
            if (selectedOption == null)
                return Result.NotFound();

            attempt.MarkAnswer(question, selectedOption);

            await quizAttemptRepository.UpdateAsync(attempt, cancellationToken);

            return new ReplyQuizAttemptQuestionResultDto(question.IsCorrect);
        }
    }
}
