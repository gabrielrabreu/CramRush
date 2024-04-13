using Cramming.Domain.TopicAggregate;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.Pdfing.Components
{
    public class DefaultHeaderComponent(Topic model) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.PaddingBottom(24).Row(_ =>
            {
                _.RelativeItem().Column(_ =>
                {
                    _.Item().Element(ComposeAppName);
                    _.Item().Element(ComposeTopicName);
                    _.Item().Element(ComposeTopicTags);
                });
            });
        }

        private void ComposeAppName(IContainer _)
        {
            _.Text(_ =>
            {
                _.Span("Cramming")
                 .FontSize(20)
                 .SemiBold()
                 .FontColor(Colors.Blue.Medium);
            });
        }

        private void ComposeTopicName(IContainer _)
        {
            _.Text(_ =>
            {
                _.Span("Name: ")
                 .SemiBold();

                _.Span(model.Name);
            });
        }

        private void ComposeTopicTags(IContainer _)
        {
            _.Text(_ =>
            {
                _.Span("Tags: ")
                 .SemiBold();

                if (model.HasTags())
                {
                    foreach (var tag in model.Tags)
                    {
                        _.Span(tag.Name)
                         .BackgroundColor(tag.Colour?.Code ?? Colors.White)
                         .FontColor(Colors.Black);

                        _.Span(";");
                    }
                }
                else
                {
                    _.Span("-");
                }
            });
        }
    }
}
