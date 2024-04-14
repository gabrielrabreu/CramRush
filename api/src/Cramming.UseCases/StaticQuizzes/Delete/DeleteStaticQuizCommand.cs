using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Delete
{
    public record DeleteStaticQuizCommand(Guid StaticQuizId) : ICommand<Result>
    {
    }
}
