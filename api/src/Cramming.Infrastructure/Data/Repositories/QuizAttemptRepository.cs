using Cramming.Domain.QuizAttemptAggregate;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.Infrastructure.Data.Repositories
{
    public class QuizAttemptRepository(AppDbContext db) : EfRepository<QuizAttempt>(db), IQuizAttemptRepository
    {
        public override async Task<QuizAttempt?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var quiz = await base.GetByIdAsync(id, cancellationToken);

            if (quiz != null)
            {
                await Db.Entry(quiz).Collection(p => p.Questions).LoadAsync(cancellationToken);

                foreach (var question in quiz.Questions)
                    await Db.Entry(question).Collection(p => p.Options).LoadAsync(cancellationToken);
            }

            return quiz;
        }
    }
}
