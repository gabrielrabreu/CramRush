using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Pdf.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.Pdf.Documents
{
    public class NotecardsDocument(Topic model) : IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(_ =>
            {
                _.Margin(50);
                _.Header().Component(new DefaultHeaderComponent(model));
                _.Content().Element(ComposeFrontContent);
                _.Footer().Component<DefaultFooterComponent>();
            });

            container.Page(_ =>
            {
                _.Margin(50);
                _.Header().Component(new DefaultHeaderComponent(model));
                _.Content().Element(ComposeBackContent);
                _.Footer().Component<DefaultFooterComponent>();
            });
        }

        private void ComposeContent(IContainer _, Action<TableDescriptor, uint, Question, Question?> action)
        {
            _.Table(_ =>
            {
                _.ColumnsDefinition(_ =>
                {
                    _.RelativeColumn();
                    _.RelativeColumn();
                });

                var row = 1u;
                for (int i = 0; i < model.Questions.Count; i += 2)
                {
                    var leftQuestion = model.Questions.ElementAt(i);
                    var rightQuestion = i + 1 < model.Questions.Count ? model.Questions.ElementAt(i + 1) : null;
                    action(_, row, leftQuestion, rightQuestion);
                    row++;
                }
            });
        }

        private void ComposeFrontContent(IContainer _)
        {
            ComposeContent(_, (table, row, leftQuestion, rightQuestion) =>
            {
                table.Cell()
                    .Row(row)
                    .Column(1)
                    .Element(Block)
                    .Text(leftQuestion.Statement);

                if (rightQuestion != null)
                    table.Cell()
                        .Row(row)
                        .Column(2)
                        .Element(Block)
                        .Text(rightQuestion.Statement);
            });
        }

        private void ComposeBackContent(IContainer _)
        {
            ComposeContent(_, (table, row, leftQuestion, rightQuestion) =>
            {
                table.Cell()
                    .Row(row)
                    .Column(1)
                    .Element(Block)
                    .Element(_ => { ComposeAnswer(_, leftQuestion); });

                if (rightQuestion != null)
                    table.Cell()
                        .Row(row)
                        .Column(2)
                        .Element(Block)
                        .Element(_ => { ComposeAnswer(_, rightQuestion); });
            });
        }

        private static IContainer Block(IContainer _)
        {
            return _
                .Border(1)
                .Background(Colors.Grey.Lighten3)
                .MinWidth(50)
                .MinHeight(100)
                .Padding(1)
                .AlignCenter()
                .AlignMiddle();
        }

        private static void ComposeAnswer(IContainer _, Question question)
        {
            if (question is OpenEndedQuestion openEndedQuestion)
                ComposeAnswer(_, openEndedQuestion);
            if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                ComposeAnswer(_, multipleChoiceQuestion);
        }

        private static void ComposeAnswer(IContainer _, OpenEndedQuestion question)
        {
            _.Text(question.Answer);
        }

        private static void ComposeAnswer(IContainer _, MultipleChoiceQuestion question)
        {
            var answers = question.Options
                .Where(c => c.IsAnswer)
                .Select(s => s.Statement);

            _.Text(string.Join(";", answers));
        }
    }
}
