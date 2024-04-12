using Cramming.Domain.TopicAggregate;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PDF.Documents
{
    public abstract class TopicDocument(Topic topic)
    {
        protected readonly Topic Topic = topic;

        protected static void DescribeTitle(TextDescriptor text)
        {
            text.Span(DocumentConstants.AppName)
                .FontSize(20)
                .SemiBold()
                .FontColor(Colors.Blue.Medium);
        }

        protected void DescribeName(TextDescriptor text)
        {
            text.Span($"{nameof(Topic.Name)} {DocumentConstants.Colon}").SemiBold();
            text.Span(Topic.Name);
        }

        protected void DescribeTags(TextDescriptor text)
        {
            text.Span($"{nameof(Topic.Tags)} {DocumentConstants.Colon}").SemiBold();

            if (Topic.HasTags())
            {
                foreach (var tag in Topic.Tags)
                {
                    text.Span(tag.Name)
                        .BackgroundColor(tag.Colour.Code)
                        .FontColor(Colors.Black);
                    text.Span(DocumentConstants.Semicolon);
                }
            }
            else
            {
                text.Span(DocumentConstants.Dash);
            }
        }

        protected void ComposeHeader(IContainer container)
        {
            container.PaddingBottom(24).Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(DescribeTitle);
                    column.Item().Text(DescribeName);
                    column.Item().Text(DescribeTags);
                });
            });
        }

        protected void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
        }

        protected static IContainer Block(IContainer container)
        {
            return container
                .Border(1)
                .Background(Colors.Grey.Lighten3)
                .MinWidth(50)
                .MinHeight(100)
                .Padding(1)
                .AlignCenter()
                .AlignMiddle();
        }
    }
}
