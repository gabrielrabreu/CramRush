using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Mappings
{
    public class TopicOpenEndedQuestionMapping : IEntityTypeConfiguration<TopicOpenEndedQuestionData>
    {
        public void Configure(EntityTypeBuilder<TopicOpenEndedQuestionData> builder)
        {
            builder.ToTable("TopicOpenEndedQuestions");

            builder.Property(p => p.Answer).IsRequired();

            builder.HasBaseType<TopicQuestionData>();
        }
    }
}
