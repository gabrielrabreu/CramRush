using Cramming.Application.Topics.Queries;
using Cramming.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.DocumentComposer.Documents
{
    public class NotecardDocument(TopicDetailDto topic) : BaseTopicDocument(topic), IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(ComposeFront);
            container.Page(ComposeBack);
        }

        public void ComposeFront(PageDescriptor page)
        {
            page.Margin(50);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeFrontContent);
            page.Footer().Element(ComposeFooter);
        }

        public void ComposeBack(PageDescriptor page)
        {
            page.Margin(50);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeBackContent);
            page.Footer().Element(ComposeFooter);
        }

        private void ComposeHeader(IContainer container)
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

        private void ComposeFrontContent(IContainer container)
        {
            ComposeContent(container, (table, row, question1, question2) =>
            {
                table.Cell().Row(row).Column(1).Element(Block).Text(question1.Statement);
                if (question2 != null)
                    table.Cell().Row(row).Column(2).Element(Block).Text(question2.Statement);
            });
        }

        private void ComposeBackContent(IContainer container)
        {
            ComposeContent(container, (table, row, question1, question2) =>
            {
                table.Cell().Row(row).Column(1).Element(Block).Element(e =>
                {
                    GetQuestionComponent(question1).Compose(e);
                });

                if (question2 != null)
                    table.Cell().Row(row).Column(2).Element(Block).Element(e =>
                    {
                        GetQuestionComponent(question2).Compose(e);
                    });
            });
        }

        public static IComponent GetQuestionComponent(TopicDetailQuestionDto Question)
        {
            return Question.Type switch
            {
                QuestionType.MultipleChoice => new MultipleChoiceQuestionNotecardComponent(Question),
                _ => new OpenEndedQuestionNotecardComponent(Question),
            };
        }

        private void ComposeContent(IContainer container, Action<TableDescriptor, uint, TopicDetailQuestionDto, TopicDetailQuestionDto?> action)
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
                    var question1 = Topic.Questions[i];
                    var question2 = i + 1 < Topic.Questions.Count ? Topic.Questions[i + 1] : null;

                    action(table, row, question1, question2);

                    row++;
                }
            });
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

        static IContainer Block(IContainer container)
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


    public class OpenEndedQuestionNotecardComponent(TopicDetailQuestionDto Question) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.Text(Question.Answer);
        }
    }

    public class MultipleChoiceQuestionNotecardComponent(TopicDetailQuestionDto Question) : IComponent
    {
        public void Compose(IContainer container)
        {
            var answers = Question.Options?.Where(c => c.IsAnswer).Select(s => s.Statement) ?? [];
            container.Text(string.Join(";", answers));
        }
    }
}
