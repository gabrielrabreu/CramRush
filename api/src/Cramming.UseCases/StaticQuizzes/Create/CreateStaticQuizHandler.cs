using Cramming.Domain.StaticQuizAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Create
{
    public class CreateStaticQuizHandler(IStaticQuizRepository repository) : ICommandHandler<CreateStaticQuizCommand, Result<StaticQuizBriefDto>>
    {
        public async Task<Result<StaticQuizBriefDto>> Handle(CreateStaticQuizCommand request, CancellationToken cancellationToken)
        {
            var newQuiz = new StaticQuiz(request.Title);

            foreach (var questionDto in request.Questions)
            {
                var newQuestion = new StaticQuizQuestion(questionDto.Statement);

                foreach (var optionDto in questionDto.Options)
                    newQuestion.AddOption(new StaticQuizQuestionOption(optionDto.Text, optionDto.IsCorrect));

                newQuiz.AddQuestion(newQuestion);
            }

            var savedQuiz = await repository.AddAsync(newQuiz, cancellationToken);

            return new StaticQuizBriefDto(savedQuiz.Id, savedQuiz.Title, savedQuiz.Questions.Count);
        }
    }
}
