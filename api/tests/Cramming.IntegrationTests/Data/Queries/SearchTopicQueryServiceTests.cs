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
        public async Task SearchTopicsWithEmpty()
        {
            fixture.ClearDatabase();

            var result = await service.SearchAsync(1, 2);

            result.Items.Should().BeEmpty();
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(0);
            result.TotalCount.Should().Be(0);
            result.HasPreviousPage.Should().BeFalse();
            result.HasNextPage.Should().BeFalse();
        }

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
            result.Items.Should().SatisfyRespectively(
                item1 => { item1.Name.Should().Be("Topic 1"); }, 
                item2 => { item2.Name.Should().Be("Topic 2"); });
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
            result.Items.Should().SatisfyRespectively(
                item1 => { item1.Name.Should().Be("Topic 3"); },
                item2 => { item2.Name.Should().Be("Topic 4"); });
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
            result.Items.Should().SatisfyRespectively(
                item1 => { item1.Name.Should().Be("Topic 5"); },
                item2 => { item2.Name.Should().Be("Topic 6"); });
            result.PageNumber.Should().Be(3);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeFalse();
        }
    }
}
