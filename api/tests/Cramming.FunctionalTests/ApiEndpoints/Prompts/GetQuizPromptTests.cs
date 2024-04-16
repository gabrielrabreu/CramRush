using Cramming.API.Prompts;
using Cramming.API.Quizzes;
using Cramming.FunctionalTests.Support;

namespace Cramming.FunctionalTests.ApiEndpoints.Prompts
{
    public class GetQuizPromptTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOk()
        {
            var route = GetQuizPrompt.BuildRoute();

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<GetQuizPromptResponse>(_output);
            result.Should().NotBeNull();
            result.Prompt.Should().NotBeEmpty();
            result.Location.Should().Be(CreateQuiz.Route);
        }
    }
}
