using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetPracticeTest
{
    public class GetPracticeTestHandler(
        ITopicReadRepository repository,
        IPracticeTestPdfService service) : IQueryHandler<GetPracticeTestQuery, Result<BinaryContent>>
    {
        public async Task<Result<BinaryContent>> Handle(GetPracticeTestQuery request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return service.Create(topic);
        }
    }
}
