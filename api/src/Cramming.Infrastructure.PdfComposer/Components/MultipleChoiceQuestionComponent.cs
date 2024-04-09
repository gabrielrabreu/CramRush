using Cramming.Application.Topics.Queries;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PdfComposer.Components
{
    public class MultipleChoiceQuestionComponent(TopicDetailQuestionDto Question) : IComponent
    {
        public void Compose(IContainer container)
        {
            container.PaddingBottom(10).Column(column =>
            {
                column.Item().PaddingBottom(5).Text(Question.Statement).SemiBold();

                foreach (var item in Question.Choices!)
                {
                    column.Item().Text($"(  ) {item.Statement}").SemiBold();
                }
            });
        }
    }
}
