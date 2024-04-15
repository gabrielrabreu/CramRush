using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.DownloadFlashcards
{
    public record DownloadStaticQuizFlashcardsQuery(Guid StaticQuizId) : IQuery<Result<BinaryContent>>;
}
