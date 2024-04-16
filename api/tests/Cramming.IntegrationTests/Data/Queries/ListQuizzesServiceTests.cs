using Cramming.Domain.QuizAggregate;
using Cramming.Infrastructure.Data;
using Cramming.Infrastructure.Data.Queries;

namespace Cramming.IntegrationTests.Data.Queries
{
    public class ListQuizzesServiceTests
        (EfTestFixture fixture) 
        : IClassFixture<EfTestFixture>
    {
        private readonly EfRepository<Quiz> repository = new(fixture.Db);
        private readonly ListQuizzesService service = new(fixture.Db);

        [Fact]
        public async Task ListQuizzesWithEmpty()
        {
            fixture.ClearDatabase();

            var result = await service.ListAsync(1, 2);

            result.Items.Should().BeEmpty();
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(0);
            result.TotalCount.Should().Be(0);
            result.HasPreviousPage.Should().BeFalse();
            result.HasNextPage.Should().BeFalse();
        }

        [Fact]
        public async Task ListQuizzesWithFirstPage()
        {
            fixture.ClearDatabase();

            await repository.AddAsync(new Quiz("Sample Title 1"));
            await repository.AddAsync(new Quiz("Sample Title 2"));
            await repository.AddAsync(new Quiz("Sample Title 3"));
            await repository.AddAsync(new Quiz("Sample Title 4"));
            await repository.AddAsync(new Quiz("Sample Title 5"));
            await repository.AddAsync(new Quiz("Sample Title 6"));

            var result = await service.ListAsync(1, 2);

            result.Items.Should().HaveCount(2);
            result.Items.Should().SatisfyRespectively(
                item1 => { item1.Title.Should().Be("Sample Title 1"); },
                item2 => { item2.Title.Should().Be("Sample Title 2"); });
            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeFalse();
            result.HasNextPage.Should().BeTrue();
        }

        [Fact]
        public async Task ListQuizzesByMiddlePage()
        {
            fixture.ClearDatabase();

            await repository.AddAsync(new Quiz("Sample Title 1"));
            await repository.AddAsync(new Quiz("Sample Title 2"));
            await repository.AddAsync(new Quiz("Sample Title 3"));
            await repository.AddAsync(new Quiz("Sample Title 4"));
            await repository.AddAsync(new Quiz("Sample Title 5"));
            await repository.AddAsync(new Quiz("Sample Title 6"));

            var result = await service.ListAsync(2, 2);

            result.Items.Should().HaveCount(2);
            result.Items.Should().SatisfyRespectively(
                item1 => { item1.Title.Should().Be("Sample Title 3"); },
                item2 => { item2.Title.Should().Be("Sample Title 4"); });
            result.PageNumber.Should().Be(2);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeTrue();
        }

        [Fact]
        public async Task ListQuizzesByLastPage()
        {
            fixture.ClearDatabase();

            await repository.AddAsync(new Quiz("Sample Title 1"));
            await repository.AddAsync(new Quiz("Sample Title 2"));
            await repository.AddAsync(new Quiz("Sample Title 3"));
            await repository.AddAsync(new Quiz("Sample Title 4"));
            await repository.AddAsync(new Quiz("Sample Title 5"));
            await repository.AddAsync(new Quiz("Sample Title 6"));

            var result = await service.ListAsync(3, 2);

            result.Items.Should().HaveCount(2);
            result.Items.Should().SatisfyRespectively(
                item1 => { item1.Title.Should().Be("Sample Title 5"); },
                item2 => { item2.Title.Should().Be("Sample Title 6"); });
            result.PageNumber.Should().Be(3);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(6);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeFalse();
        }
    }
}
