using Cramming.Domain.QuizAttemptAggregate;
using Cramming.Infrastructure.Pdf.Components;

namespace Cramming.Infrastructure.Pdf.Documents
{
    public class QuizAttemptToReplyDocument(QuizAttempt model) : IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(_ =>
            {
                _.Margin(50);
                _.Header().Component(new DefaultHeaderComponent(model.QuizTitle));
                _.Content().Element(ComposeContent);
                _.Footer().Component<DefaultFooterComponent>();
            });
        }

        private void ComposeContent(IContainer _)
        {
            _.PaddingTop(10)
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

        private static void ComposeQuestion(IContainer _, QuizAttemptQuestion question)
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
                            .Text($"(  ) {option.Text}")
                            .SemiBold();
                });
        }
    }
}
