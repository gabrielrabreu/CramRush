namespace Cramming.Infrastructure.Pdf.Components
{
    public class DefaultHeaderComponent(string title) : IComponent
    {
        public string Title { get; set; } = title;

        public void Compose(IContainer container)
        {
            container.PaddingBottom(24).Row(_ =>
            {
                _.RelativeItem().Column(_ =>
                {
                    _.Item().Element(ComposeAppName);
                    _.Item().Element(ComposeTitle);
                });
            });
        }

        private void ComposeAppName(IContainer _)
        {
            _.Text(_ =>
            {
                _.Span("Cramming")
                 .FontSize(20)
                 .SemiBold()
                 .FontColor(Colors.Blue.Medium);
            });
        }

        private void ComposeTitle(IContainer _)
        {
            _.Text(_ =>
            {
                _.Span(Title);
            });
        }
    }
}
