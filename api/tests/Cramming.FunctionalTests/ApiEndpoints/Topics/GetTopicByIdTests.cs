using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class GetTopicByIdTests(CustomWebApplicationFactory factory, ITestOutputHelper output) 
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOkGivenExistingTopic()
        {
            var existingTopic = await EnsureExistingTopic();

            var route = GetTopicById.BuildRoute(existingTopic.Id);

            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<TopicDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTopic()
        {
            var route = GetTopicById.BuildRoute(Guid.NewGuid());
            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        private async Task<TopicBriefDto> EnsureExistingTopic()
        {
            var route = CreateTopic.Route;
            var request = new CreateTopicRequest() { Name = "Topic" };
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);
            var result = await response.DeserializeAsync<TopicBriefDto>(_output);
            return result;
        }
    }
}
