using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Data;
using Cramming.Infrastructure.Data.Queries;

namespace Cramming.IntegrationTests.Data.Queries
{
    public class SearchTopicQueryServiceTests(EfTestFixture fixture) : IClassFixture<EfTestFixture>
    {
        private readonly EfRepository<Topic> repository = new(fixture.Db);
        private readonly SearchTopicQueryService service = new(fixture.Db);

        [Fact]
        public async Task SearchTopicsWithFirstPage()
        {
            fixture.ClearDatabase();

            await repository.AddAsync(new Topic("Topic 1"));
            await repository.AddAsync(new Topic("Topic 2"));
            await repository.AddAsync(new Topic("Topic 3"));
            await repository.AddAsync(new Topic("Topic 4"));
            await repository.AddAsync(new Topic("Topic 5"));
            await repository.AddAsync(new Topic("Topic 6"));

            var result = await service.SearchAsync(1, 2);

            result.Items.Should().HaveCount(2);
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeFalse();
            result.HasNextPage.Should().BeTrue();
        }

        [Fact]
        public async Task SearchTopicsByMiddlePage()
        {
            fixture.ClearDatabase();

            await repository.AddAsync(new Topic("Topic 1"));
            await repository.AddAsync(new Topic("Topic 2"));
            await repository.AddAsync(new Topic("Topic 3"));
            await repository.AddAsync(new Topic("Topic 4"));
            await repository.AddAsync(new Topic("Topic 5"));
            await repository.AddAsync(new Topic("Topic 6"));

            var result = await service.SearchAsync(2, 2);

            result.Items.Should().HaveCount(2);
            result.PageNumber.Should().Be(2);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeTrue();
        }

        [Fact]
        public async Task SearchTopicsByLastPage()
        {
            fixture.ClearDatabase();

            await repository.AddAsync(new Topic("Topic 1"));
            await repository.AddAsync(new Topic("Topic 2"));
            await repository.AddAsync(new Topic("Topic 3"));
            await repository.AddAsync(new Topic("Topic 4"));
            await repository.AddAsync(new Topic("Topic 5"));
            await repository.AddAsync(new Topic("Topic 6"));

            var result = await service.SearchAsync(3, 2);

            result.Items.Should().HaveCount(2);
            result.PageNumber.Should().Be(3);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeFalse();
        }
    }
}
