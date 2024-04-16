namespace Cramming.UseCases.Quizzes.DownloadFlashcards
{
    public class DownloadQuizFlashcardsHandler(
        IQuizReadRepository repository,
        IQuizFlashcardsPdfService service)
        : IQueryHandler<DownloadQuizFlashcardsQuery, Result<BinaryContent>>
    {
        public async Task<Result<BinaryContent>> Handle(
            DownloadQuizFlashcardsQuery request,
            CancellationToken cancellationToken)
        {
            var quiz = await repository.GetByIdAsync(
                request.QuizId,
                cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            return service.Create(quiz);
        }
    }
}
