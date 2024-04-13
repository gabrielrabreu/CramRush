using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Pdf.Documents;
using Cramming.SharedKernel;
using Cramming.UseCases.Topics.GetNotecards;
using QuestPDF.Fluent;

namespace Cramming.Infrastructure.Pdfing
{
    public class NotecardsPdfService : INotecardsPdfService
    {
        public BinaryContent Create(Topic model)
        {
            return BinaryContent.Pdf(
                new NotecardsDocument(model).GeneratePdf(),
                $"Notecards_{model.Name.Replace(" ", "")}_{DateTime.UtcNow}");
        }
    }
}
