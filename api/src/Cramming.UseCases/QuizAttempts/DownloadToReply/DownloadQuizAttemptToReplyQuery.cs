using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.DownloadToReply
{
    public record DownloadQuizAttemptToReplyQuery(Guid QuizAttemptId) : IQuery<Result<BinaryContent>>;
}
