using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class DeleteTopicTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsNoContentGivenExistingTopic()
        {
            var existingTopic = await EnsureExistingTopic();

            var route = DeleteTopic.BuildRoute(existingTopic.Id);
            var response = await _client.ExecuteDeleteAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNoContent();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTopic()
        {
            var route = DeleteTopic.BuildRoute(Guid.NewGuid());
            var response = await _client.ExecuteDeleteAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        private async Task<TopicBriefDto> EnsureExistingTopic()
        {
            var route = CreateTopic.Route;
            var request = new CreateTopicRequest() { Name = "Topic" };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<TopicBriefDto>(_output);
        }
    }
}
