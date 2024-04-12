using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cramming.IntegrationTests.Data.Repositories
{
    public class TopicRepositoryTests(EfTestFixture fixture) : IClassFixture<EfTestFixture>
    {
        private readonly TopicRepository repository = new(fixture.Db);

        [Fact]
        public async Task GetsTopicsById()
        {
            fixture.ClearDatabase();

            var topic1 = new Topic("Topic 1");
            await repository.AddAsync(topic1);
            fixture.Db.Entry(topic1).State = EntityState.Detached;

            var topic2 = new Topic("Topic 2");
            await repository.AddAsync(topic2);
            fixture.Db.Entry(topic2).State = EntityState.Detached;

            (await repository.GetByIdAsync(topic1.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(topic2.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(Guid.NewGuid())).Should().BeNull();
        }

        [Fact]
        public async Task GetsTopicsByIdWithChildren()
        {
            fixture.ClearDatabase();

            var topic = new Topic("Topic 1");
            topic.AddTag("Tag Name", "Tag Colour");
            topic.AddOpenEndedQuestion("Statement 1", "Answer 1");
            topic.AddMultipleChoiceQuestion("Statement 2").AddOption("Option 1", true);
            fixture.Db.Entry(topic).State = EntityState.Detached;

            var id = (await repository.AddAsync(topic)).Id;

            var createdItem = await repository.GetByIdAsync(id);
            createdItem.Should().NotBeNull();
            createdItem!.Tags.Should().HaveCount(1);
            createdItem.Questions.Should().HaveCount(2);
            createdItem.Questions.Should().SatisfyRespectively(
                first =>
                {
                    first.Should().BeOfType<OpenEndedQuestion>();
                },
                second =>
                {
                    second.Should().BeOfType<MultipleChoiceQuestion>()
                        .Which.Options.Should().HaveCount(1);
                });
        }
    }
}
