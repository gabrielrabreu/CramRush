using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.DeleteTag
{
    public class DeleteTagHandler(ITopicRepository repository) : ICommandHandler<DeleteTagCommand, Result>
    {
        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(request.TopicId, cancellationToken);

            if (topic == null)
                return Result.NotFound();

            if (topic.DoesNotHaveTag(request.Id))
                return Result.NotFound();

            topic.DeleteTag(request.Id);

            await repository.UpdateAsync(topic, cancellationToken);

            return Result.OK();
        }
    }
}
