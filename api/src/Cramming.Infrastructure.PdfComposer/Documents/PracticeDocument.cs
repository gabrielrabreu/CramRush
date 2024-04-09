using Cramming.Application.Topics.Queries;
using Cramming.Infrastructure.PdfComposer.Components;
using Cramming.Infrastructure.PdfComposer.Composers;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PdfComposer.Documents
{
    public class PracticeDocument(TopicDetailDto topic) : BaseTopicDocument(topic), IDocument
    {
        private readonly TopicDetailDto Topic = topic;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(DescribeTitle);
                    column.Item().Text(DescribeName);
                    column.Item().Text(DescribeTags);
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Component(new QuestionsComponent(Topic.Questions));
        }

        private void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
        }
    }
}
