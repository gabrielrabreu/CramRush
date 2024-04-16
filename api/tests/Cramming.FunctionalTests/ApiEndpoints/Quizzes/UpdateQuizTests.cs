using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Quizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.Quizzes
{
    public class UpdateQuizTests(
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

            var route = UpdateQuiz.BuildRoute(existingQuiz.Id);
            var request = new UpdateQuizRequest()
            {
                Title = "Updated Title",
                Questions =
                [
                    new UpdateQuizQuestionRequest
                    {
                        Statement = "Updated Statement",
                        Options = [
                            new UpdateQuizQuestionOptionRequest {
                                Text = "Updated Text",
                                IsCorrect = true
                            }
                       ]
                    }
                ]
            };

            var response = await _client.ExecutePutAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureNoContent();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingQuiz()
        {
            var route = UpdateQuiz.BuildRoute(Guid.NewGuid());
            var request = new UpdateQuizRequest() { Title = "Updated Title", Questions = [] };

            var response = await _client.ExecutePutAsync(route, request, _output);
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
