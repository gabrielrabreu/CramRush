using Cramming.Domain.StaticQuizAggregate;
using Cramming.Infrastructure.Data.Repositories;

namespace Cramming.IntegrationTests.Data.Repositories
{
    public class StaticQuizRepositoryTests(EfTestFixture fixture) : IClassFixture<EfTestFixture>
    {
        private readonly StaticQuizRepository repository = new(fixture.Db);

        [Fact]
        public async Task GetsStaticQuizById()
        {
            fixture.ClearDatabase();

            var quiz1 = new StaticQuiz("Sample Title 1");
            await repository.AddAsync(quiz1);
            fixture.Db.Entry(quiz1).State = EntityState.Detached;

            var quiz2 = new StaticQuiz("Sample Title 2");
            await repository.AddAsync(quiz2);
            fixture.Db.Entry(quiz2).State = EntityState.Detached;

            (await repository.GetByIdAsync(quiz1.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(quiz2.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(Guid.NewGuid())).Should().BeNull();
        }

        [Fact]
        public async Task GetsStaticQuizByIdWithChildren()
        {
            fixture.ClearDatabase();

            var quiz = new StaticQuiz("Sample Title");
            var question = new StaticQuizQuestion("Sample Statement");
            question.AddOption(new StaticQuizQuestionOption("Sample Text 1", true));
            question.AddOption(new StaticQuizQuestionOption("Sample Text 2", true));
            quiz.AddQuestion(question);

            fixture.Db.Entry(quiz).State = EntityState.Detached;

            var id = (await repository.AddAsync(quiz)).Id;

            var createdItem = await repository.GetByIdAsync(id);
            createdItem.Should().NotBeNull();
            createdItem!.Questions.Should().HaveCount(1);
            createdItem.Questions.Should().SatisfyRespectively(
                first =>
                {
                    first.Options.Should().HaveCount(2);
                });
        }
    }
}
