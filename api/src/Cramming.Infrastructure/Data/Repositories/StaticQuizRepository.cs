using Cramming.Domain.StaticQuizAggregate;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.Infrastructure.Data.Repositories
{
    public class StaticQuizRepository(AppDbContext db) : EfRepository<StaticQuiz>(db), IStaticQuizRepository
    {
        public override async Task<StaticQuiz?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
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
