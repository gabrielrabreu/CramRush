using Cramming.Domain.TopicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Config
{
    public class MultipleChoiceQuestionOptionConfiguration : IEntityTypeConfiguration<MultipleChoiceQuestionOption>
    {
        public void Configure(EntityTypeBuilder<MultipleChoiceQuestionOption> builder)
        {
            builder.Property(question => question.Statement).IsRequired();

            builder.Property(question => question.IsAnswer).IsRequired();
        }
    }
}
