using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Cramming.Infrastructure.Pdfing.Components
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
