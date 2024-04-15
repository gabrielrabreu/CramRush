using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.Delete
{
    public class DeleteStaticQuizHandler(IStaticQuizRepository repository) : ICommandHandler<DeleteStaticQuizCommand, Result>
    {
        public async Task<Result> Handle(DeleteStaticQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = await repository.GetByIdAsync(request.StaticQuizId, cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            await repository.DeleteAsync(quiz, cancellationToken);

            return Result.OK();
        }
    }
}
