using Cramming.Application.Topics.Queries;
using Cramming.Domain.Common.Exceptions;
using Cramming.Domain.Enums;
using Cramming.Infrastructure.Resources;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.PdfComposer.Components
{
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
                QuestionType.OpenEnded => new OpenEndedQuestionComponent(Question),
                QuestionType.MultipleChoice => new MultipleChoiceQuestionComponent(Question),
                _ => throw new DomainRuleException(nameof(Question.Type), string.Format(MyResources.UnknownQuestionType, Question.Type)),
            };
        }
    }
}
