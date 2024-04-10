using Cramming.Application.Topics.Queries;
using Cramming.Infrastructure.Resources;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Cramming.Infrastructure.DocumentComposer.Documents
{
    public abstract class BaseTopicDocument(TopicDetailDto topic)
    {
        protected readonly TopicDetailDto Topic = topic;

        protected static void DescribeTitle(TextDescriptor text)
        {
            text.Span("Cramming")
                .FontSize(20)
                .SemiBold()
                .FontColor(Colors.Blue.Medium);
        }

        protected void DescribeName(TextDescriptor text)
        {
            text.Span($"{MyResources.TopicName}: ").SemiBold();
            text.Span($"{Topic.Name}");
        }

        protected void DescribeTags(TextDescriptor text)
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
                        .BackgroundColor(tag.Colour)
                        .FontColor(Colors.Black);
                    text.Span("; ");
                }
            }
        }
    }
}
