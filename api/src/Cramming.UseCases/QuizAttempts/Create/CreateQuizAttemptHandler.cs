using Cramming.Domain.QuizAttemptAggregate;
using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.UseCases.QuizAttempts.Create
{
    public class CreateQuizAttemptHandler(
        IStaticQuizReadRepository staticQuizReadRepository,
        IQuizAttemptRepository quizAttemptRepository) : ICommandHandler<CreateQuizAttemptCommand, Result<QuizAttemptDto>>
    {
        public async Task<Result<QuizAttemptDto>> Handle(CreateQuizAttemptCommand request, CancellationToken cancellationToken)
        {
            var quiz = await staticQuizReadRepository.GetByIdAsync(request.QuizId, cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            var newAttempt = new QuizAttempt(quiz.Title);

            foreach (var question in quiz.Questions)
            {
                var newAttemptQuestion = new QuizAttemptQuestion(question.Statement);

                foreach (var option in question.Options)
                    newAttemptQuestion.AddOption(new QuizAttemptQuestionOption(option.Text, option.IsCorrect));

                newAttempt.AddQuestion(newAttemptQuestion);
            }

            var savedAttempt = await quizAttemptRepository.AddAsync(newAttempt, cancellationToken);

            return new QuizAttemptDto(
                savedAttempt.Id,
                savedAttempt.QuizTitle,
                savedAttempt.IsPending,
                savedAttempt.Questions.Select(
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
