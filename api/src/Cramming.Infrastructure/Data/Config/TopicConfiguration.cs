using Cramming.Domain.TopicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Config
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(topic => topic.Name)
                .HasMaxLength(DataSchemaConstants.DEFAULT_TOPIC_NAME_LENGTH)
                .IsRequired();

            builder.HasMany(topic => topic.Tags)
                .WithOne(tag => tag.Topic)
                .HasForeignKey(tag => tag.TopicId);
        }
    }
}
