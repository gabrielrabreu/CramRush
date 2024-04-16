using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizAttemptQuestionConfiguration
        : IEntityTypeConfiguration<QuizAttemptQuestion>
    {
        public void Configure(
            EntityTypeBuilder<QuizAttemptQuestion> builder)
        {
            builder.Property(question => question.Statement)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(question => question.Options)
                .WithOne(option => option.Question)
                .HasForeignKey(option => option.QuestionId);
        }
    }
}
