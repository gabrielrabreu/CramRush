using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Mappings
{
    public class TopicMapping : IEntityTypeConfiguration<TopicData>
    {
        public void Configure(EntityTypeBuilder<TopicData> builder)
        {
            builder.ToTable("Topics");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Description).IsRequired();

            builder.HasMany(n => n.Tags).WithOne(n => n.Topic).HasForeignKey(f => f.TopicId);

            builder.HasMany(n => n.Questions).WithOne(n => n.Topic).HasForeignKey(f => f.TopicId);
        }
    }
}
