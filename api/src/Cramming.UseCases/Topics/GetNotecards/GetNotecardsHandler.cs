using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetNotecards
{
    public class GetNotecardsHandler(
        ITopicReadRepository repository,
        INotecardsPDFService service) : IQueryHandler<GetNotecardsQuery, Result<Document>>
    {
        public async Task<Result<Document>> Handle(GetNotecardsQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return await service.ComposeAsync(topic, cancellationToken);
        }
    }

}
