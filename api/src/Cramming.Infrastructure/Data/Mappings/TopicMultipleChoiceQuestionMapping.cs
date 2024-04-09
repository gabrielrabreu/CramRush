using Cramming.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cramming.Infrastructure.Data.Mappings
{
    public class TopicMultipleChoiceQuestionMapping : IEntityTypeConfiguration<TopicMultipleChoiceQuestionData>
    {
        public void Configure(EntityTypeBuilder<TopicMultipleChoiceQuestionData> builder)
        {
            builder.ToTable("TopicMultipleChoiceQuestions");

            builder.HasMany(n => n.Options).WithOne(n => n.Question).HasForeignKey(f => f.QuestionId);

            builder.HasBaseType<TopicQuestionData>();
        }
    }
}
