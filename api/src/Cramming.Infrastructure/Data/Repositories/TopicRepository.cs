using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;

namespace Cramming.Infrastructure.Data.Repositories
{
    public class TopicRepository(AppDbContext db) : EfRepository<Topic>(db), ITopicRepository
    {
        public override async Task<Topic?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var topic = await base.GetByIdAsync(id, cancellationToken);

            if (topic != null)
            {
                await Db.Entry(topic).Collection(p => p.Tags).LoadAsync(cancellationToken);
                await Db.Entry(topic).Collection(p => p.Questions).LoadAsync(cancellationToken);

                foreach (var question in topic.Questions)
                {
                    if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                        await Db.Entry(multipleChoiceQuestion).Collection(p => p.Options).LoadAsync(cancellationToken);
                }
            }

            return topic;
        }
    }
}
