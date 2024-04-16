namespace Cramming.UseCases.QuizAttempts.DownloadToReply
{
    public class DownloadQuizAttemptToReplyHandler(
        IQuizAttemptReadRepository repository,
        IQuizAttemptToReplyPdfService service)
        : IQueryHandler<DownloadQuizAttemptToReplyQuery, Result<BinaryContent>>
    {
        public async Task<Result<BinaryContent>> Handle(
            DownloadQuizAttemptToReplyQuery request,
            CancellationToken cancellationToken)
        {
            var topic = await repository.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken);

            if (topic == null)
                return Result.NotFound();

            return service.Create(topic);
        }
    }
}
