using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetPracticeTest
{
    public class GetPracticeTestHandler(
        ITopicReadRepository repository,
        IPracticeTestPDFService service) : IQueryHandler<GetPracticeTestQuery, Result<Document>>
    {
        public async Task<Result<Document>> Handle(GetPracticeTestQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return await service.ComposeAsync(topic, cancellationToken);
        }
    }
}
