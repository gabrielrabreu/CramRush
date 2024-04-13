using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cramming.IntegrationTests.Data
{
    public class EfRepositoryTests(EfTestFixture fixture) : IClassFixture<EfTestFixture>
    {
        private readonly EfRepository<Topic> repository = new(fixture.Db);

        [Fact]
        public async Task GetsAllTopics()
        {
            fixture.ClearDatabase();

            var topic1 = new Topic("Topic 1");
            await repository.AddAsync(topic1);
            fixture.Db.Entry(topic1).State = EntityState.Detached;

            var topic2 = new Topic("Topic 2");
            await repository.AddAsync(topic2);
            fixture.Db.Entry(topic2).State = EntityState.Detached;

            (await repository.GetAllAsync()).Should().HaveCount(2);
        }

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
        public async Task AddsTopicAndSetsId()
        {
            fixture.ClearDatabase();

            var topic = new Topic("Topic Name");
            await repository.AddAsync(topic);
            fixture.Db.Entry(topic).State = EntityState.Detached;

            var createdItem = (await repository.GetAllAsync()).SingleOrDefault();
            createdItem.Should().NotBeNull();
            createdItem!.Id.Should().NotBeEmpty();
            createdItem.Name.Should().Be(topic.Name);
            createdItem.Tags.Should().BeEmpty();
            createdItem.Questions.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdatesItemAfterAddingIt()
        {
            fixture.ClearDatabase();

            var topic = new Topic("Topic Name");
            await repository.AddAsync(topic);
            fixture.Db.Entry(topic).State = EntityState.Detached;

            var createdItem = (await repository.GetAllAsync()).SingleOrDefault();
            createdItem.Should().NotBeNull();
            createdItem!.Should().NotBe(topic);
            createdItem!.UpdateName("New Topic Name");

            await repository.UpdateAsync(createdItem);

            var updatedItem = (await repository.GetAllAsync()).SingleOrDefault();
            updatedItem.Should().NotBeNull();
            updatedItem!.Id.Should().NotBeEmpty();
            updatedItem.Name.Should().NotBe(topic.Name);
            updatedItem.Name.Should().Be(createdItem.Name);
            updatedItem.Tags.Should().BeEmpty();
            updatedItem.Questions.Should().BeEmpty();
        }

        [Fact]
        public async Task DeletesItemAfterAddingIt()
        {
            fixture.ClearDatabase();

            var topic = new Topic("Topic Name");
            await repository.AddAsync(topic);
            fixture.Db.Entry(topic).State = EntityState.Detached;

            var createdItem = (await repository.GetAllAsync()).SingleOrDefault();
            createdItem.Should().NotBeNull();
            await repository.DeleteAsync(createdItem!);

            var deletedItem = (await repository.GetAllAsync()).SingleOrDefault();
            deletedItem.Should().BeNull();
        }
    }
}
