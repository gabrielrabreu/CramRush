using Cramming.Domain.TopicAggregate;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PDF.Documents
{
    public class PracticeDocument(Topic topic) : TopicDocument(topic), IDocument
    {
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

        private void ComposeContent(IContainer container)
        {
            container.PaddingTop(40).Column(column =>
            {
                for (int i = 0; i < Topic.Questions.Count; i += 2)
                {
                    var leftQuestion = Topic.Questions.ElementAt(i);
                    var rightQuestion = i + 1 < Topic.Questions.Count ? Topic.Questions.ElementAt(i + 1) : null;

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Element(e =>
                        {
                            ComposeQuestion(leftQuestion, e);
                        });

                        row.ConstantItem(50);

                        if (rightQuestion != null)
                            row.RelativeItem().Element(e =>
                            {
                                ComposeQuestion(rightQuestion, e);
                            });
                    });
                }
            });
        }

        private void ComposeQuestion(Question question, IContainer container)
        {
            if (question is OpenEndedQuestion openEndedQuestion)
                ComposeQuestion(openEndedQuestion, container);
            if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                ComposeQuestion(multipleChoiceQuestion, container);
        }

        private void ComposeQuestion(OpenEndedQuestion question, IContainer container)
        {
            container.PaddingBottom(40).Column(column =>
            {
                column.Item().PaddingBottom(5).Text(question.Statement).SemiBold();
            });
        }

        private void ComposeQuestion(MultipleChoiceQuestion question, IContainer container)
        {
            container.PaddingBottom(10).Column(column =>
            {
                column.Item().PaddingBottom(5).Text(question.Statement).SemiBold();
                foreach (var option in question.Options)
                    column.Item().Text($"(  ) {option.Statement}").SemiBold();
            });
        }
    }
}
