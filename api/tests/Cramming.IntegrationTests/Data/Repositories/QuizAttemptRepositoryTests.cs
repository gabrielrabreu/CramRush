using Cramming.Domain.QuizAttemptAggregate;
using Cramming.Infrastructure.Data.Repositories;

namespace Cramming.IntegrationTests.Data.Repositories
{
    public class QuizAttemptRepositoryTests(EfTestFixture fixture) : IClassFixture<EfTestFixture>
    {
        private readonly QuizAttemptRepository repository = new(fixture.Db);

        [Fact]
        public async Task GetsQuizById()
        {
            fixture.ClearDatabase();

            var quizAttempt1 = new QuizAttempt("Sample Title 1");
            await repository.AddAsync(quizAttempt1);
            fixture.Db.Entry(quizAttempt1).State = EntityState.Detached;

            var quizAttempt2 = new QuizAttempt("Sample Title 2");
            await repository.AddAsync(quizAttempt2);
            fixture.Db.Entry(quizAttempt2).State = EntityState.Detached;

            (await repository.GetByIdAsync(quizAttempt1.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(quizAttempt2.Id)).Should().NotBeNull();
            (await repository.GetByIdAsync(Guid.NewGuid())).Should().BeNull();
        }

        [Fact]
        public async Task GetsQuizByIdWithChildren()
        {
            fixture.ClearDatabase();

            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            quizAttemptQuestion.AddOption(new QuizAttemptQuestionOption("Sample Text 1", true));
            quizAttemptQuestion.AddOption(new QuizAttemptQuestionOption("Sample Text 2", true));
            quizAttempt.AddQuestion(quizAttemptQuestion);

            fixture.Db.Entry(quizAttempt).State = EntityState.Detached;

            var id = (await repository.AddAsync(quizAttempt)).Id;

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
