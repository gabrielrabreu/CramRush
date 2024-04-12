using Cramming.Domain.TopicAggregate;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PDF.Documents
{
    public class NotecardsDocument(Topic topic) : TopicDocument(topic), IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeFrontContent);
                page.Footer().Element(ComposeFooter);
            });

            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeBackContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeFrontContent(IContainer container)
        {
            ComposeContent(container, (table, row, leftQuestion, rightQuestion) =>
            {
                table.Cell().Row(row).Column(1).Element(Block).Text(leftQuestion.Statement);
                if (rightQuestion != null)
                    table.Cell().Row(row).Column(2).Element(Block).Text(rightQuestion.Statement);
            });
        }

        private void ComposeBackContent(IContainer container)
        {
            ComposeContent(container, (table, row, leftQuestion, rightQuestion) =>
            {
                table.Cell().Row(row).Column(1).Element(Block).Element(e =>
                {
                    ComposeAnswer(leftQuestion, e);
                });

                if (rightQuestion != null)
                    table.Cell().Row(row).Column(2).Element(Block).Element(e =>
                    {
                        ComposeAnswer(rightQuestion, e);
                    });
            });
        }

        private void ComposeContent(IContainer container, Action<TableDescriptor, uint, Question, Question?> action)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                var row = 1u;
                for (int i = 0; i < Topic.Questions.Count; i += 2)
                {
                    var leftQuestion = Topic.Questions.ElementAt(i);
                    var rightQuestion = i + 1 < Topic.Questions.Count ? Topic.Questions.ElementAt(i + 1) : null;

                    action(table, row, leftQuestion, rightQuestion);

                    row++;
                }
            });
        }

        private void ComposeAnswer(Question question, IContainer container)
        {
            if (question is OpenEndedQuestion openEndedQuestion)
                ComposeAnswer(openEndedQuestion, container);
            if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                ComposeAnswer(multipleChoiceQuestion, container);
        }

        private void ComposeAnswer(OpenEndedQuestion question, IContainer container)
        {
            container.Text(question.Answer);
        }

        private void ComposeAnswer(MultipleChoiceQuestion question, IContainer container)
        {
            var answers = question.Options.Where(c => c.IsAnswer).Select(s => s.Statement);
            container.Text(string.Join(DocumentConstants.Semicolon, answers));
        }
    }
}
