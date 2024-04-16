using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizAttemptQuestionOptionConfiguration
        : IEntityTypeConfiguration<QuizAttemptQuestionOption>
    {
        public void Configure(
            EntityTypeBuilder<QuizAttemptQuestionOption> builder)
        {
            builder.Property(question => question.Text)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
