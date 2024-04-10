using Cramming.Application.Common.Interfaces;
using Cramming.Application.Notecards.Interfaces;
using Cramming.Application.Topics.Queries;
using Cramming.Infrastructure.DocumentComposer.Documents;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.DocumentComposer.Composers
{

    public class NotecardComposer : INotecardComposer
    {
        public FileComposed Compose(TopicDetailDto topic)
        {
            Settings.License = LicenseType.Community;

            var document = new NotecardDocument(topic);

            var content = document.GeneratePdf();

            return new FileComposed(content, "application/pdf", "Notecards.pdf");
        }
    }
}
