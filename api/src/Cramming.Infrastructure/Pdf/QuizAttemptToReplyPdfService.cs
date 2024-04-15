using Cramming.Domain.QuizAttemptAggregate;
using Cramming.Infrastructure.Pdf.Documents;
using Cramming.SharedKernel;
using Cramming.UseCases.QuizAttempts.DownloadToReply;
using QuestPDF.Fluent;

namespace Cramming.Infrastructure.Pdf
{
    public class QuizAttemptToReplyPdfService : IQuizAttemptToReplyPdfService
    {
        public BinaryContent Create(QuizAttempt model)
        {
            var content = new QuizAttemptToReplyDocument(model).GeneratePdf();
            var name = $"Reply_{model.QuizTitle.RemoveSpacesAndAccents()}_{DateTime.UtcNow:s}";

            return BinaryContent.Pdf(content, name);
        }
    }
}
