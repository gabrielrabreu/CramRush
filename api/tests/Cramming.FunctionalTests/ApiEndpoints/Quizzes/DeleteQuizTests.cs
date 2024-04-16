using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Quizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.Quizzes
{
    public class DeleteQuizTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsNoContentGivenExistingQuiz()
        {
            var existingQuiz = await EnsureExistingQuiz();

            var route = DeleteQuiz.BuildRoute(existingQuiz.Id);
            var response = await _client.ExecuteDeleteAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNoContent();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingQuiz()
        {
            var route = DeleteQuiz.BuildRoute(Guid.NewGuid());
            var response = await _client.ExecuteDeleteAsync(route, _output);
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
