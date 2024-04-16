using Cramming.Domain.QuizAggregate;
using Cramming.Infrastructure.Data;

namespace Cramming.IntegrationTests.Data
{
    public class EfRepositoryTests(EfTestFixture fixture) : IClassFixture<EfTestFixture>
    {
        private readonly EfRepository<Quiz> repository = new(fixture.Db);

        [Fact]
        public async Task GetsAllQuizzes()
        {
            fixture.ClearDatabase();

            var quiz1 = new Quiz("Sample Title 1");
            await repository.AddAsync(quiz1);
            fixture.Db.Entry(quiz1).State = EntityState.Detached;

            var quiz2 = new Quiz("Sample Title 2");
            await repository.AddAsync(quiz2);
            fixture.Db.Entry(quiz2).State = EntityState.Detached;

            (await repository.GetAllAsync()).Should().HaveCount(2);
        }

        [Fact]
        public async Task GetsQuizById()
        {
            fixture.ClearDatabase();

            var quiz1 = new Quiz("Sample Title 1");
            await repository.AddAsync(quiz1);
            fixture.Db.Entry(quiz1).State = EntityState.Detached;

            var quiz2 = new Quiz("Sample Title 2");
            await repository.AddAsync(quiz2);
            fixture.Db.Entry(quiz2).State = EntityState.Detached;

            (await repository.GetByIdAsync(quiz1.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(quiz2.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(Guid.NewGuid())).Should().BeNull();
        }

        [Fact]
        public async Task AddsQuizAndSetsId()
        {
            fixture.ClearDatabase();

            var quiz = new Quiz("Sample Title");
            await repository.AddAsync(quiz);
            fixture.Db.Entry(quiz).State = EntityState.Detached;

            var createdItem = (await repository.GetAllAsync()).SingleOrDefault();
            createdItem.Should().NotBeNull();
            createdItem!.Id.Should().NotBeEmpty();
            createdItem.Title.Should().Be(quiz.Title);
            createdItem.Questions.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdatesItemAfterAddingIt()
        {
            fixture.ClearDatabase();

            var quiz = new Quiz("Sample Title");
            await repository.AddAsync(quiz);
            fixture.Db.Entry(quiz).State = EntityState.Detached;

            var createdItem = (await repository.GetAllAsync()).SingleOrDefault();
            createdItem.Should().NotBeNull();
            createdItem!.Should().NotBe(quiz);
            createdItem!.SetTitle("New Title");

            await repository.UpdateAsync(createdItem);

            var updatedItem = (await repository.GetAllAsync()).SingleOrDefault();
            updatedItem.Should().NotBeNull();
            updatedItem!.Id.Should().NotBeEmpty();
            updatedItem.Title.Should().NotBe(quiz.Title);
            updatedItem.Title.Should().Be(createdItem.Title);
            updatedItem.Questions.Should().BeEmpty();
        }

        [Fact]
        public async Task DeletesItemAfterAddingIt()
        {
            fixture.ClearDatabase();

            var quiz = new Quiz("Sample Title");
            await repository.AddAsync(quiz);
            fixture.Db.Entry(quiz).State = EntityState.Detached;

            var createdItem = (await repository.GetAllAsync()).SingleOrDefault();
            createdItem.Should().NotBeNull();
            await repository.DeleteAsync(createdItem!);

            var deletedItem = (await repository.GetAllAsync()).SingleOrDefault();
            deletedItem.Should().BeNull();
        }
    }
}
