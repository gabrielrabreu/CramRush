using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.UpdateTag
{
    public class UpdateTagHandler(ITopicRepository repository) : ICommandHandler<UpdateTagCommand, Result>
    {
        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            if (topic.DoesNotHaveTag(request.Id))
                return Result.NotFound();

            topic.UpdateTagName(request.Id, request.Name);
            topic.UpdateTagColour(request.Id, request.Colour);

            await repository.UpdateAsync(topic, cancellationToken);

            return Result.OK();
        }
    }
}
