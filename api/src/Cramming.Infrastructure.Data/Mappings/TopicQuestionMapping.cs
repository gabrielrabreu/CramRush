using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Mappings
{
    public class TopicQuestionMapping : IEntityTypeConfiguration<TopicQuestionData>
    {
        public void Configure(EntityTypeBuilder<TopicQuestionData> builder)
        {
            builder.ToTable("TopicQuestions");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Statement).IsRequired();
        }
    }
}
