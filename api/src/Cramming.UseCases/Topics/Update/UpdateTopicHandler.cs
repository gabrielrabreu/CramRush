using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Update
{
    public class UpdateTopicHandler(ITopicRepository repository) : ICommandHandler<UpdateTopicCommand, Result>
    {
        public async Task<Result> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            topic.UpdateName(request.Name);

            await repository.UpdateAsync(topic, cancellationToken);

            return Result.OK();
        }
    }
}
