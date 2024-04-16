using Cramming.Domain.QuizAggregate;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizConfiguration
        : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(
            EntityTypeBuilder<Quiz> builder)
        {
            builder.Property(quiz => quiz.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(quiz => quiz.Questions)
                .WithOne(question => question.Quiz)
                .HasForeignKey(question => question.QuizId);
        }
    }
}
