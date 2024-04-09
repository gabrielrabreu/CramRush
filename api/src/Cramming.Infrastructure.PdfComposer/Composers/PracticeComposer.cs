using Cramming.Application.Common.Interfaces;
using Cramming.Application.Practices.Interfaces;
using Cramming.Application.Topics.Queries;
using Cramming.Infrastructure.PdfComposer.Documents;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PdfComposer.Composers
{
    public class PracticeComposer : IPracticeComposer
    {
        public FileComposed Compose(TopicDetailDto topic)
        {
            Settings.License = LicenseType.Community;

            var document = new PracticeDocument(topic);

            var content = document.GeneratePdf();

            return new FileComposed(content, "application/pdf", "Practice.pdf");
        }
    }
}
