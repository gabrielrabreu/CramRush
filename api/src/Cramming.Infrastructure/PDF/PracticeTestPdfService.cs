using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.PDF.Documents;
using Cramming.UseCases.Topics.GetPracticeTest;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PDF
{
    public class PracticeTestPdfService : IPracticeTestPdfService
    {
        public Task<SharedKernel.Document> ComposeAsync(Topic model, CancellationToken cancellationToken = default)
        {
            Settings.License = LicenseType.Community;

            var document = new PracticeDocument(model);

            byte[] bytes = document.GeneratePdf();
            string contentType = "application/pdf";
            string name = $"{nameof(Topic)}_PracticeTest_{DateTime.UtcNow}";

            return Task.FromResult(new SharedKernel.Document(bytes, contentType, name));
        }
    }
}
