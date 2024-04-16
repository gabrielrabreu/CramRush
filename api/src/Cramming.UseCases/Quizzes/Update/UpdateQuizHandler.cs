using Cramming.Domain.QuizAggregate;

namespace Cramming.UseCases.Quizzes.Update
{
    public class UpdateQuizHandler(
        IQuizRepository repository)
        : ICommandHandler<UpdateQuizCommand, Result>
    {
        public async Task<Result> Handle(
            UpdateQuizCommand request,
            CancellationToken cancellationToken)
        {
            var quiz = await repository.GetByIdAsync(
                request.QuizId,
                cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            quiz.SetTitle(request.Title);
            quiz.ClearQuestions();

            foreach (var questionDto in request.Questions)
            {
                var newQuestion = new QuizQuestion(questionDto.Statement);

                foreach (var optionDto in questionDto.Options)
                    newQuestion.AddOption(new QuizQuestionOption(optionDto.Text, optionDto.IsCorrect));

                quiz.AddQuestion(newQuestion);
            }

            await repository.UpdateAsync(
                quiz,
                cancellationToken);

            return Result.OK();
        }
    }
}
