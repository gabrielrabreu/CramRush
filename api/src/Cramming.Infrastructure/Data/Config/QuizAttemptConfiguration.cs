using Cramming.Domain.QuizAttemptAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Config
{
    public class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.Property(quiz => quiz.QuizTitle)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(quiz => quiz.IsPending)
                .IsRequired();

            builder.HasMany(quiz => quiz.Questions)
                .WithOne(question => question.QuizAttempt)
                .HasForeignKey(question => question.QuizAttemptId);
        }
    }

    public class QuizAttemptQuestionConfiguration : IEntityTypeConfiguration<QuizAttemptQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizAttemptQuestion> builder)
        {
            builder.Property(question => question.Statement)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(quiz => quiz.IsPending)
                .IsRequired();

            builder.HasMany(question => question.Options)
                .WithOne(option => option.Question)
                .HasForeignKey(option => option.QuestionId);
        }
    }

    public class QuizAttemptQuestionOptionConfiguration : IEntityTypeConfiguration<QuizAttemptQuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuizAttemptQuestionOption> builder)
        {
            builder.Property(question => question.Text)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(question => question.IsCorrect)
                .IsRequired();

            builder.Property(question => question.IsSelected)
                .IsRequired();
        }
    }
}
