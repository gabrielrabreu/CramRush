using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.CreateTag
{
    public class CreateTagHandler(ITopicRepository repository) : ICommandHandler<CreateTagCommand, Result<TagDto>>
    {
        public async Task<Result<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            var tag = topic.AddTag(request.Name, request.Colour);

            await repository.UpdateAsync(topic, cancellationToken);

            return new TagDto(tag.Id, tag.TopicId, tag.Name, tag.Colour?.Code);
        }
    }
}
