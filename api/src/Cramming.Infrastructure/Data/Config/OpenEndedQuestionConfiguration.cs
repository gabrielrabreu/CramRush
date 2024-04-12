using Cramming.Domain.TopicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Config
{
    public class OpenEndedQuestionConfiguration : IEntityTypeConfiguration<OpenEndedQuestion>
    {
        public void Configure(EntityTypeBuilder<OpenEndedQuestion> builder)
        {
            builder.HasBaseType<Question>();

            builder.Property(question => question.Answer).IsRequired();
        }
    }
}
