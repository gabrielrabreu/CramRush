using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Mappings
{
    public class TopicMultipleChoiceQuestionOptionMapping : IEntityTypeConfiguration<TopicMultipleChoiceQuestionOptionData>
    {
        public void Configure(EntityTypeBuilder<TopicMultipleChoiceQuestionOptionData> builder)
        {
            builder.ToTable("TopicMultipleChoiceQuestionOptions");

            builder.Property(p => p.Statement).IsRequired();

            builder.Property(p => p.IsAnswer).IsRequired();
        }
    }
}
