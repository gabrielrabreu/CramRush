using Cramming.Application.Topics.Queries;
using Cramming.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.DocumentComposer.Documents
{
    public class PracticeDocument(TopicDetailDto topic) : BaseTopicDocument(topic), IDocument
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

    public class QuestionsComponent(IList<TopicDetailQuestionDto> Questions) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.PaddingTop(40).Column(column =>
            {
                for (int i = 0; i < Questions.Count; i += 2)
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Component(GetQuestionComponent(Questions[i]));
                        row.ConstantItem(50);

                        if (i + 1 < Questions.Count)
                            row.RelativeItem().Component(GetQuestionComponent(Questions[i + 1]));
                    });
                }
            });
        }

        public static IComponent GetQuestionComponent(TopicDetailQuestionDto Question)
        {
            return Question.Type switch
            {
                QuestionType.MultipleChoice => new MultipleChoiceQuestionPracticeComponent(Question),
                _ => new OpenEndedQuestionPracticeComponent(Question),
            };
        }
    }

    public class OpenEndedQuestionPracticeComponent(TopicDetailQuestionDto Question) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.PaddingBottom(40).Column(column =>
            {
                column.Item().PaddingBottom(5).Text(Question.Statement).SemiBold();
            });
        }
    }
    public class MultipleChoiceQuestionPracticeComponent(TopicDetailQuestionDto Question) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.PaddingBottom(10).Column(column =>
            {
                column.Item().PaddingBottom(5).Text(Question.Statement).SemiBold();

                foreach (var option in Question.Options!)
                {
                    column.Item().Text($"(  ) {option.Statement}").SemiBold();
                }
            });
        }
    }
}
