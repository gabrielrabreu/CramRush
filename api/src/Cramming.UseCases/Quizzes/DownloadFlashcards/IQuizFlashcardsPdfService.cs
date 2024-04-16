using Cramming.Domain.QuizAggregate;

namespace Cramming.UseCases.Quizzes.DownloadFlashcards
{
    public interface IQuizFlashcardsPdfService
        : IPdfService<Quiz>;
}
