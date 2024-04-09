using Cramming.Application.Topics.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PdfComposer.Components
{
    public class OpenEndedQuestionComponent(TopicDetailQuestionDto Question) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.PaddingBottom(40).Column(column =>
            {
                column.Item().PaddingBottom(5).Text(Question.Statement).SemiBold();
            });
        }
    }
}
