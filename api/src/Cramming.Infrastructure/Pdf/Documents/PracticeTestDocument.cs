using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Pdf.Components;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.Pdf.Documents
{
    public class PracticeTestDocument(Topic model) : IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(_ =>
            {
                _.Margin(50);
                _.Header().Component(new DefaultHeaderComponent(model));
                _.Content().Element(ComposeContent);
                _.Footer().Component<DefaultFooterComponent>();
            });
        }

        private void ComposeContent(IContainer _)
        {
            _.PaddingTop(40)
                .Column(_ =>
                {
                    for (int i = 0; i < model.Questions.Count; i += 2)
                    {
                        var leftQuestion = model.Questions.ElementAt(i);
                        var rightQuestion = i + 1 < model.Questions.Count ? model.Questions.ElementAt(i + 1) : null;

                        _.Item()
                            .Row(_ =>
                            {
                                _.RelativeItem()
                                    .Element(_ => { ComposeQuestion(_, leftQuestion); });

                                _.ConstantItem(50);

                                if (rightQuestion != null)
                                    _.RelativeItem()
                                        .Element(_ => { ComposeQuestion(_, rightQuestion); });
                            });
                    }
                });
        }

        private static void ComposeQuestion(IContainer _, Question question)
        {
            if (question is OpenEndedQuestion openEndedQuestion)
                ComposeQuestion(_, openEndedQuestion);
            if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                ComposeQuestion(_, multipleChoiceQuestion);
        }

        private static void ComposeQuestion(IContainer _, OpenEndedQuestion question)
        {
            _.PaddingBottom(40)
                .Column(_ =>
                {
                    _.Item()
                        .PaddingBottom(5)
                        .Text(question.Statement)
                        .SemiBold();
                });
        }

        private static void ComposeQuestion(IContainer _, MultipleChoiceQuestion question)
        {
            _.PaddingBottom(10)
                .Column(_ =>
                {
                    _.Item()
                        .PaddingBottom(5)
                        .Text(question.Statement)
                        .SemiBold();

                    foreach (var option in question.Options)
                        _.Item()
                            .Text($"(  ) {option.Statement}")
                            .SemiBold();
                });
        }
    }
}
