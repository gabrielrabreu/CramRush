using Cramming.Domain.QuizAggregate;
using Cramming.UseCases.Quizzes;

namespace Cramming.Infrastructure.Data.Repositories
{
    public class QuizRepository(
        AppDbContext db)
        : EfRepository<Quiz>(db), IQuizRepository
    {
        public override async Task<Quiz?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var quiz = await base.GetByIdAsync(
                id,
                cancellationToken);

            if (quiz != null)
            {
                await Db.Entry(quiz)
                    .Collection(p => p.Questions)
                    .LoadAsync(cancellationToken);

                foreach (var question in quiz.Questions)
                    await Db.Entry(question)
                        .Collection(p => p.Options)
                        .LoadAsync(cancellationToken);
            }

            return quiz;
        }
    }
}
