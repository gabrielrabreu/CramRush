using Cramming.Domain.StaticQuizAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Config
{
    public class StaticQuizConfiguration : IEntityTypeConfiguration<StaticQuiz>
    {
        public void Configure(EntityTypeBuilder<StaticQuiz> builder)
        {
            builder.Property(quiz => quiz.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(quiz => quiz.Questions)
                .WithOne(question => question.Quiz)
                .HasForeignKey(question => question.QuizId);
        }
    }

    public class StaticQuizQuestionConfiguration : IEntityTypeConfiguration<StaticQuizQuestion>
    {
        public void Configure(EntityTypeBuilder<StaticQuizQuestion> builder)
        {
            builder.Property(question => question.Statement)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(question => question.Options)
                .WithOne(option => option.Question)
                .HasForeignKey(option => option.QuestionId);
        }
    }

    public class StaticQuizQuestionOptionConfiguration : IEntityTypeConfiguration<StaticQuizQuestionOption>
    {
        public void Configure(EntityTypeBuilder<StaticQuizQuestionOption> builder)
        {
            builder.Property(question => question.Text)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(question => question.IsCorrect)
                .IsRequired();
        }
    }
}
