using Cramming.Domain.QuizAttemptAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.QuizAttempts.DownloadToReply
{
    public interface IQuizAttemptToReplyPdfService : IPdfService<QuizAttempt>;
}
