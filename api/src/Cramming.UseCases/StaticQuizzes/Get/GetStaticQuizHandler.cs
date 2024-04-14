using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Get
{
    public class GetStaticQuizHandler(IStaticQuizReadRepository readRepository) : IQueryHandler<GetStaticQuizQuery, Result<StaticQuizDto>>
    {
        public async Task<Result<StaticQuizDto>> Handle(GetStaticQuizQuery request, CancellationToken cancellationToken)
        {
            var quiz = await readRepository.GetByIdAsync(request.StaticQuizId, cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            return new StaticQuizDto(
                quiz.Id,
                quiz.Title,
                quiz.Questions.Select(question =>
                    new StaticQuizQuestionDto(
                        question.Id,
                        question.Statement,
                        question.Options.Select(option =>
                            new StaticQuizQuestionOptionDto(
                                option.Id,
                                option.Text,
                                option.IsCorrect)))));
        }
    }
}
