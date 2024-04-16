using Cramming.Domain.QuizAggregate;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizQuestionConfiguration
        : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(
            EntityTypeBuilder<QuizQuestion> builder)
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
