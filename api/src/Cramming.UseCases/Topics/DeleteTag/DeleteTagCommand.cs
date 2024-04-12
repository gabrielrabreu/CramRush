using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.DeleteTag
{
    public record DeleteTagCommand(Guid Id, Guid TopicId) : ICommand<Result>
    {
    }
}
