using Cramming.API.QuizAttempts;
using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.Quizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.QuizAttempts
{
    public class CreateQuizAttemptTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsCreatedGivenValidRequest()
        {
            var existingQuiz = await EnsureExistingQuiz();

            var route = CreateQuizAttempt.BuildRoute();
            var request = new CreateQuizAttemptRequest
            {
                QuizId = existingQuiz.Id
            };

            var response = await _client.ExecutePostAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureCreated();

            var result = await response.DeserializeAsync<QuizAttemptDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.QuizTitle.Should().Be(existingQuiz.Title);
            result.Questions.Should().HaveCount(existingQuiz.TotalQuestions);
            response.EnsureLocation(GetQuizAttempt.BuildRoute(result.Id), _output);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingQuiz()
        {
            var route = CreateQuizAttempt.BuildRoute();
            var request = new CreateQuizAttemptRequest
            {
                QuizId = Guid.NewGuid()
            };
            var response = await _client.ExecutePostAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        private async Task<QuizBriefDto> EnsureExistingQuiz()
        {
            var route = CreateQuiz.BuildRoute();
            var request = new CreateQuizRequest
            {
                Title = "Sample Title",
                Questions =
                [
                    new CreateQuizQuestionRequest
                    {
                        Statement = "Sample Statement",
                        Options = [
                            new CreateQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = true
                            }
                       ]
                    }
                ]
            };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<QuizBriefDto>(_output);
        }
    }
}
