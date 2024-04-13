using Cramming.API.Prompts;
using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;

namespace Cramming.FunctionalTests.ApiEndpoints.Prompts
{
    public class GetReplaceQuestionsPromptTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOk()
        {
            var route = GetReplaceQuestionsPrompt.Route;

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<GetReplaceQuestionsPromptResponse>(_output);
            result.Should().NotBeNull();
            result.Prompt.Should().NotBeEmpty();
            result.Location.Should().Be(ReplaceQuestions.Route);
        }
    }
}
