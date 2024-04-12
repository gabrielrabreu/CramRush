using Cramming.Domain.TopicAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetNotecards
{
    public interface INotecardsPdfService : IDocumentService<Topic>
    {
    }
}
