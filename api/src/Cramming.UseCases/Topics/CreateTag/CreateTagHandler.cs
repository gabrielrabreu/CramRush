using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.CreateTag
{
    public class CreateTagHandler(ITopicRepository repository) : ICommandHandler<CreateTagCommand, Result<TagDTO>>
    {
        public async Task<Result<TagDTO>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            var tag = topic.AddTag(request.Name, request.Colour);

            await repository.UpdateAsync(topic, cancellationToken);

            return new TagDTO(tag.Id, tag.TopicId, tag.Name, tag.Colour?.Code);
        }
    }
}
