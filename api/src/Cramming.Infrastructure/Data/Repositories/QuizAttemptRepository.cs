using Cramming.Domain.QuizAttemptAggregate;
using Cramming.UseCases.QuizAttempts;

namespace Cramming.Infrastructure.Data.Repositories
{
    public class QuizAttemptRepository(
        AppDbContext db)
        : EfRepository<QuizAttempt>(db), IQuizAttemptRepository
    {
        public override async Task<QuizAttempt?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var quizAttempt = await base.GetByIdAsync(
                id,
                cancellationToken);

            if (quizAttempt != null)
            {
                await Db.Entry(quizAttempt)
                    .Collection(p => p.Questions)
                    .LoadAsync(cancellationToken);

                foreach (var question in quizAttempt.Questions)
                    await Db.Entry(question)
                        .Collection(p => p.Options)
                        .LoadAsync(cancellationToken);
            }

            return quizAttempt;
        }
    }
}
