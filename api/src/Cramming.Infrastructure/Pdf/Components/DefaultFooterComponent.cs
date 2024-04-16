namespace Cramming.Infrastructure.Pdf.Components
{
    public class DefaultFooterComponent : IComponent
    {
        public void Compose(IContainer container)
        {
            container.AlignCenter().Text(_ =>
            {
                _.CurrentPageNumber();
                _.Span(" / ");
                _.TotalPages();
            });
        }
    }
}
