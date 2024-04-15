using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.UseCases.QuizAttempts.Get
{
    public class GetQuizAttemptHandler(IQuizAttemptReadRepository readRepository) : IQueryHandler<GetQuizAttemptQuery, Result<QuizAttemptDto>>
    {
        public async Task<Result<QuizAttemptDto>> Handle(GetQuizAttemptQuery request, CancellationToken cancellationToken)
        {
            var attempt = await readRepository.GetByIdAsync(request.QuizAttemptId, cancellationToken);

            if (attempt == null)
                return Result.NotFound();

            return new QuizAttemptDto(
                attempt.Id,
                attempt.QuizTitle,
                attempt.IsPending,
                attempt.Questions.Select(
                    question => new QuizAttemptQuestionDto(
                        question.Id,
                        question.Statement,
                        question.IsPending,
                        question.Options.Select(
                            option => new QuizAttemptQuestionOptionDto(
                                option.Id,
                                option.Text,
                                option.IsSelected)))));
        }
    }
}
