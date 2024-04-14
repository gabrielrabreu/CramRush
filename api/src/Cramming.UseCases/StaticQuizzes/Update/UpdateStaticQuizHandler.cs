using Cramming.Domain.StaticQuizAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Update
{
    public class UpdateStaticQuizHandler(IStaticQuizRepository repository) : ICommandHandler<UpdateStaticQuizCommand, Result>
    {
        public async Task<Result> Handle(UpdateStaticQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = await repository.GetByIdAsync(request.StaticQuizId, cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            quiz.SetTitle(request.Title);
            quiz.ClearQuestions();

            foreach (var questionDto in request.Questions)
            {
                var newQuestion = new StaticQuizQuestion(questionDto.Statement);

                foreach (var optionDto in questionDto.Options)
                    newQuestion.AddOption(new StaticQuizQuestionOption(optionDto.Text, optionDto.IsCorrect));

                quiz.AddQuestion(newQuestion);
            }

            await repository.UpdateAsync(quiz, cancellationToken);

            return Result.OK();
        }
    }
}
