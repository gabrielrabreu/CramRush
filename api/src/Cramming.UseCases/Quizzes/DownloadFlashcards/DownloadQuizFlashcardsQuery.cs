namespace Cramming.UseCases.Quizzes.DownloadFlashcards
{
    public record DownloadQuizFlashcardsQuery(
        Guid QuizId)
        : IQuery<Result<BinaryContent>>;
}
