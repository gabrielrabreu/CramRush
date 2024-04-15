using Cramming.Domain.StaticQuizAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes.DownloadFlashcards
{
    public interface IStaticQuizFlashcardsPdfService : IPdfService<StaticQuiz>;
}
