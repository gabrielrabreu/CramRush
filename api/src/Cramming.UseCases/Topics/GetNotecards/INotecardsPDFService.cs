using Cramming.Domain.TopicAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetNotecards
{
    public interface INotecardsPDFService : IDocumentService<Topic>
    {
    }
}
