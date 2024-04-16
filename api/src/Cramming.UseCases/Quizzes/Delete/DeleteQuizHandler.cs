namespace Cramming.UseCases.Quizzes.Delete
{
    public class DeleteQuizHandler(
        IQuizRepository repository)
        : ICommandHandler<DeleteQuizCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteQuizCommand request,
            CancellationToken cancellationToken)
        {
            var quiz = await repository.GetByIdAsync(
                request.QuizId,
                cancellationToken);

            if (quiz == null)
                return Result.NotFound();

            await repository.DeleteAsync(
                quiz,
                cancellationToken);

            return Result.OK();
        }
    }
}
