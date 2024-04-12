using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Create
{
    public class CreateTopicHandler(ITopicRepository repository) : ICommandHandler<CreateTopicCommand, Result<TopicBriefDto>>
    {
        public async Task<Result<TopicBriefDto>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Topic(request.Name);

            var created = await repository.AddAsync(topic, cancellationToken);

            return new TopicBriefDto(created.Id, created.Name);
        }
    }
}
