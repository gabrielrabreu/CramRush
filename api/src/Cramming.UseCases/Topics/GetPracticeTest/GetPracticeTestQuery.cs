using Cramming.SharedKernel;

namespace Cramming.UseCases.Topics.GetPracticeTest
{
    public record GetPracticeTestQuery(Guid TopicId)
        : IQuery<Result<Document>>
    {
    }
}
