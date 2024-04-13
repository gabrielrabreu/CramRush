using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetNotecards
{
    public class GetNotecardsHandler(
        ITopicReadRepository repository,
        INotecardsPdfService service) : IQueryHandler<GetNotecardsQuery, Result<BinaryContent>>
    {
        public async Task<Result<BinaryContent>> Handle(GetNotecardsQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return service.Create(topic);
        }
    }

}
