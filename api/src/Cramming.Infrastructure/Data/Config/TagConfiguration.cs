using Cramming.Domain.TopicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Config
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(tag => tag.Name)
                .HasMaxLength(DataSchemaConstants.DEFAULT_TAG_NAME_LENGTH)
                .IsRequired();

            builder.OwnsOne(tag => tag.Colour);
        }
    }
}
