using Cramming.Application.Topics.Queries;
using Cramming.Infrastructure.Resources;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Cramming.Infrastructure.PdfComposer.Documents
{
    public abstract class BaseTopicDocument(TopicDetailDto Topic)
    {
        public static void DescribeTitle(TextDescriptor text)
        {
            text.Span("Cramming")
                .FontSize(20)
                .SemiBold()
                .FontColor(Colors.Blue.Medium);
        }

        public void DescribeName(TextDescriptor text)
        {
            text.Span($"{MyResources.TopicName}: ").SemiBold();
            text.Span($"{Topic.Name}");
        }

        public void DescribeTags(TextDescriptor text)
        {
            text.Span("Tags: ").SemiBold();

            if (Topic.Tags.Count == 0)
            {
                text.Span("-");
            }
            else
            {
                foreach (var tag in Topic.Tags)
                {
                    text.Span(tag.Name)
                        .BackgroundColor(tag.Color)
                        .FontColor(Colors.Black);
                    text.Span("; ");
                }
            }
        }
    }
}
