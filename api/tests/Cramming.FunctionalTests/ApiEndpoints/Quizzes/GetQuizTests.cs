using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.Get;

namespace Cramming.FunctionalTests.ApiEndpoints.Quizzes
{
    public class GetQuizTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOkGivenExistingQuiz()
        {
            var existingQuiz = await EnsureExistingQuiz();

            var route = GetQuiz.BuildRoute(existingQuiz.Id);

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<QuizDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingQuiz()
        {
            var route = GetQuiz.BuildRoute(Guid.NewGuid());
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
    }
}
