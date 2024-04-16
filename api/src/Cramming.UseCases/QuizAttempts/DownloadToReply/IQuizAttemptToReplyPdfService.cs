using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.UseCases.QuizAttempts.DownloadToReply
{
    public interface IQuizAttemptToReplyPdfService
        : IPdfService<QuizAttempt>;
}
