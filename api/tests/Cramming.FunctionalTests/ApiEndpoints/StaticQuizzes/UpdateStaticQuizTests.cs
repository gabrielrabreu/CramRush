using Cramming.API.StaticQuizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.StaticQuizzes;

namespace Cramming.FunctionalTests.ApiEndpoints.StaticQuizzes
{
    public class UpdateStaticQuizTests(
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

            var route = UpdateStaticQuiz.BuildRoute(existingQuiz.Id);
            var request = new UpdateStaticQuizRequest()
            {
                Title = "Updated Title",
                Questions =
                [
                    new UpdateStaticQuizQuestionRequest
                    {
                        Statement = "Updated Statement",
                        Options = [
                            new UpdateStaticQuizQuestionOptionRequest {
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
            var route = UpdateStaticQuiz.BuildRoute(Guid.NewGuid());
            var request = new UpdateStaticQuizRequest() { Title = "Updated Title", Questions = [] };

            var response = await _client.ExecutePutAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        private async Task<StaticQuizBriefDto> EnsureExistingQuiz()
        {
            var route = CreateStaticQuiz.Route;
            var request = new CreateStaticQuizRequest
            {
                Title = "Sample Title",
                Questions =
                [
                    new CreateStaticQuizQuestionRequest
                    {
                        Statement = "Sample Statement",
                        Options = [
                            new CreateStaticQuizQuestionOptionRequest {
                                Text = "Sample Text",
                                IsCorrect = true
                            }
                       ]
                    }
                ]
            };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<StaticQuizBriefDto>(_output);
        }
    }
}
