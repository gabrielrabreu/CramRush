using Cramming.API.Prompts;
using Cramming.API.StaticQuizzes;
using Cramming.FunctionalTests.Support;

namespace Cramming.FunctionalTests.ApiEndpoints.Prompts
{
    public class GetStaticQuizPromptTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOk()
        {
            var route = GetStaticQuizPrompt.Route;

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<GetStaticQuizPromptResponse>(_output);
            result.Should().NotBeNull();
            result.Prompt.Should().NotBeEmpty();
            result.Location.Should().Be(CreateStaticQuiz.Route);
        }
    }
}
