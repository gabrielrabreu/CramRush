using Cramming.Domain.QuizAggregate;
using Cramming.Domain.QuizAttemptAggregate;
using System.Reflection;

namespace Cramming.Infrastructure.Data
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Quiz> Quiz => Set<Quiz>();

        public DbSet<QuizAttempt> QuizAttempt => Set<QuizAttempt>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(
                modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
        }
    }
}
