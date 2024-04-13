using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class UpdateTagTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsNoContentGivenExistingTagAndValidRequest()
        {
            var existingTopic = await EnsureExistingTopic();
            var existingTag = await EnsureExistingTag(existingTopic.Id);

            var route = UpdateTag.BuildRoute(existingTopic.Id, existingTag.Id);
            var request = new UpdateTagRequest() { Name = "Updated Tag" };

            var response = await _client.ExecutePutAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureNoContent();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTag()
        {
            var existingTopic = await EnsureExistingTopic();

            var route = UpdateTag.BuildRoute(existingTopic.Id, Guid.NewGuid());
            var request = new UpdateTagRequest() { Name = "Updated Tag" };

            var response = await _client.ExecutePutAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTopic()
        {
            var route = UpdateTag.BuildRoute(Guid.NewGuid(), Guid.NewGuid());
            var request = new UpdateTagRequest() { Name = "Updated Tag" };

            var response = await _client.ExecutePutAsync(route, request, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        private async Task<TopicBriefDto> EnsureExistingTopic()
        {
            var route = CreateTopic.Route;
            var request = new CreateTopicRequest() { Name = "Topic" };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<TopicBriefDto>(_output);
        }

        private async Task<TagDto> EnsureExistingTag(Guid topicId)
        {
            var route = CreateTag.BuildRoute(topicId);
            var request = new CreateTagRequest() { Name = "Tag", Colour = "#808080" };

            var response = await _client.ExecutePostAsync(route, request, _output);

            return await response.DeserializeAsync<TagDto>(_output);
        }
    }
}
