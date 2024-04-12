using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetNotecards
{
    public record GetNotecardsQuery(Guid TopicId)
        : IQuery<Result<Document>>
    {
    }
}
