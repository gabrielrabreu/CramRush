using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Pdf.Documents;
using Cramming.SharedKernel;
using Cramming.UseCases.Topics.GetPracticeTest;
using QuestPDF.Fluent;

namespace Cramming.Infrastructure.Pdf
{
    public class PracticeTestPdfService : IPracticeTestPdfService
    {
        public BinaryContent Create(Topic model)
        {
            return BinaryContent.Pdf(
                new PracticeTestDocument(model).GeneratePdf(),
                $"PracticeTest_{model.Name.Replace(" ", "")}_{DateTime.UtcNow:s}");
        }
    }
}
