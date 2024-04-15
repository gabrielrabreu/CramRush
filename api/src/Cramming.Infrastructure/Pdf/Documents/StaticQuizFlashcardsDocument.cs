using Cramming.Domain.StaticQuizAggregate;
using Cramming.Infrastructure.Pdf.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.Pdf.Documents
{
    public class StaticQuizFlashcardsDocument(StaticQuiz model) : IDocument
    {
        private string Rendering = "Header";

        public void Compose(IDocumentContainer container)
        {
            Rendering = "FrontPage";
            container.Page(_ =>
            {
                _.Margin(50);
                _.Header().Component(new DefaultHeaderComponent(model.Title));
                _.Content().Element(ComposeContent);
                _.Footer().Component<DefaultFooterComponent>();
            });

            Rendering = "BackPage";
            container.Page(_ =>
            {
                _.Margin(50);
                _.Header().Component(new DefaultHeaderComponent(model.Title));
                _.Content().Element(ComposeContent);
                _.Footer().Component<DefaultFooterComponent>();
            });
        }

        private void ComposeContent(IContainer _)
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

                    ComposeQuestion(_, leftQuestion, row, 1, i);
                    ComposeQuestion(_, rightQuestion, row, 2, i+1);

                    row += 1;
                }
            });
        }

        private void ComposeQuestion(TableDescriptor _, StaticQuizQuestion? question, uint row, uint column, int index)
        {
            if (question != null)
            {
                //var cellHeader = _.Cell().Row(row).Column(column);
                var cellBody = _.Cell().Row(row + 1).Column(column);

                //cellHeader
                //    .Element(HeaderCell)
                //    .Text($"{model.Title}/{index+1}");

                if (Rendering == "BackPage")
                    cellBody
                        .Element(BodyCell)
                        .Text(question.Options.Single(option => option.IsCorrect).Text);
                else
                    cellBody
                        .Element(BodyCell)
                        .Text(question.Statement);
            }
        }

        private static IContainer HeaderCell(IContainer _)
        {
            return _
                .Border(1)
                .Background(Colors.Grey.Lighten3)
                .MinWidth(50)
                .Padding(2)
                .DefaultTextStyle(TextStyle.Default.FontSize(7));
        }

        private static IContainer BodyCell(IContainer _)
        {
            return _
                .Border(1)
                .Background(Colors.Grey.Lighten3)
                .MinWidth(50)
                .MinHeight(75)
                .Padding(1)
                .AlignCenter()
                .AlignMiddle()
                .DefaultTextStyle(TextStyle.Default.FontSize(10));
        }
    }
}
