using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizAttemptConfiguration
        : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(
            EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.Property(quiz => quiz.QuizTitle)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(quiz => quiz.Questions)
                .WithOne(question => question.QuizAttempt)
                .HasForeignKey(question => question.QuizAttemptId);
        }
    }
}
