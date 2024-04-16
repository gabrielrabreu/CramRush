using Cramming.Domain.QuizAggregate;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizQuestionOptionConfiguration
        : IEntityTypeConfiguration<QuizQuestionOption>
    {
        public void Configure(
            EntityTypeBuilder<QuizQuestionOption> builder)
        {
            builder.Property(question => question.Text)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
