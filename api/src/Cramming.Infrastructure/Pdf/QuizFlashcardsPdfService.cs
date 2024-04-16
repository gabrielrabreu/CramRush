using Cramming.Domain.QuizAggregate;
using Cramming.Infrastructure.Pdf.Documents;
using Cramming.UseCases.Quizzes.DownloadFlashcards;

namespace Cramming.Infrastructure.Pdf
{
    public class QuizFlashcardsPdfService : IQuizFlashcardsPdfService
    {
        public BinaryContent Create(Quiz model)
        {
            var content = new QuizFlashcardsDocument(model).GeneratePdf();
            var name = $"Flashcards_{model.Title.RemoveSpacesAndAccents()}_{DateTime.UtcNow:s}";

            return BinaryContent.Pdf(content, name);
        }
    }
}
