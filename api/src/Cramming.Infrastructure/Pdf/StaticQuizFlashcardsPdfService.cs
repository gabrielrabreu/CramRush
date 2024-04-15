using Cramming.Domain.StaticQuizAggregate;
using Cramming.Infrastructure.Pdf.Documents;
using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes.DownloadFlashcards;
using QuestPDF.Fluent;

namespace Cramming.Infrastructure.Pdf
{
    public class StaticQuizFlashcardsPdfService : IStaticQuizFlashcardsPdfService
    {
        public BinaryContent Create(StaticQuiz model)
        {
            var content = new StaticQuizFlashcardsDocument(model).GeneratePdf();
            var name = $"Flashcards_{model.Title.RemoveSpacesAndAccents()}_{DateTime.UtcNow:s}";

            return BinaryContent.Pdf(content, name);
        }
    }
}
