using Cramming.API.StaticQuizzes;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Get;

namespace Cramming.FunctionalTests.ApiEndpoints.StaticQuizzes
{
    public class GetStaticQuizTests(
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

            var route = GetStaticQuiz.BuildRoute(existingQuiz.Id);

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<StaticQuizDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingQuiz()
        {
            var route = GetStaticQuiz.BuildRoute(Guid.NewGuid());
            var response = await _client.ExecuteGetAsync(route, _output);
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
