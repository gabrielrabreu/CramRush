using Cramming.API.QuizAttempts;
using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.Quizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.QuizAttempts
{
    public class GetQuizAttemptTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOkGivenExistingQuizAttempt()
        {
            var existingQuiz = await EnsureExistingQuiz();
            var existingQuizAttempt = await EnsureExistingQuizAttempt(existingQuiz);

            var route = GetQuizAttempt.BuildRoute(existingQuizAttempt.Id);

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<QuizAttemptDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingQuizAttempt()
        {
            var route = GetQuizAttempt.BuildRoute(Guid.NewGuid());
            var response = await _client.ExecuteGetAsync(route, _output);
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

        private async Task<QuizAttemptDto> EnsureExistingQuizAttempt(QuizBriefDto quiz)
        {
            var route = CreateQuizAttempt.BuildRoute();
            var request = new CreateQuizAttemptRequest
            {
                QuizId = quiz.Id
            };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<QuizAttemptDto>(_output);
        }
    }
}
