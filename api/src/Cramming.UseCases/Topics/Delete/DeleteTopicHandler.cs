using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.Delete
{
    public class DeleteTopicHandler(ITopicRepository repository) : ICommandHandler<DeleteTopicCommand, Result>
    {
        public async Task<Result> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            await repository.DeleteAsync(topic, cancellationToken);

            return Result.OK();
        }
    }
}
