using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Mappings
{
    public class TopicTagMapping : IEntityTypeConfiguration<TopicTagData>
    {
        public void Configure(EntityTypeBuilder<TopicTagData> builder)
        {
            builder.ToTable("TopicTags");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Colour).IsRequired().HasDefaultValue("#000000");
        }
    }
}
