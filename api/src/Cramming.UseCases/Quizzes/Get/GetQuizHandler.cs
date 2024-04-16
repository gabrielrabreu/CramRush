namespace Cramming.UseCases.Quizzes.Get
{
    public class GetQuizHandler(
        IQuizReadRepository readRepository)
        : IQueryHandler<GetQuizQuery, Result<QuizDto>>
    {
        public async Task<Result<QuizDto>> Handle(
            GetQuizQuery request,
            CancellationToken cancellationToken)
        {
            var quiz = await readRepository.GetByIdAsync(
                request.QuizId,
                cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            return new QuizDto(
                quiz.Id,
                quiz.Title,
                quiz.Questions.Select(question =>
                    new QuizQuestionDto(
                        question.Id,
                        question.Statement,
                        question.Options.Select(option =>
                            new QuizQuestionOptionDto(
                                option.Id,
                                option.Text,
                                option.IsCorrect)))));
        }
    }
}
