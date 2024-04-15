using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.DownloadFlashcards
{
    public class DownloadStaticQuizFlashcardsHandler(
        IStaticQuizReadRepository repository,
        IStaticQuizFlashcardsPdfService service) : IQueryHandler<DownloadStaticQuizFlashcardsQuery, Result<BinaryContent>>
    {
        public async Task<Result<BinaryContent>> Handle(DownloadStaticQuizFlashcardsQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.StaticQuizId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return service.Create(topic);
        }
    }
}
