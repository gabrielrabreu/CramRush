using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class DeleteTagTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsNoContentGivenExistingTag()
        {
            var existingTopic = await EnsureExistingTopic();
            var existingTag = await EnsureExistingTag(existingTopic.Id);

            var route = DeleteTag.BuildRoute(existingTopic.Id, existingTag.Id);
            var response = await _client.ExecuteDeleteAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNoContent();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTag()
        {
            var existingTopic = await EnsureExistingTopic();
            var route = DeleteTag.BuildRoute(existingTopic.Id, Guid.NewGuid());
            var response = await _client.ExecuteDeleteAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTopic()
        {
            var route = DeleteTag.BuildRoute(Guid.NewGuid(), Guid.NewGuid());
            var response = await _client.ExecuteDeleteAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureNotFound();
        }

        private async Task<TopicBriefDto> EnsureExistingTopic()
        {
            var route = CreateTopic.Route;
            var request = new CreateTopicRequest() { Name = "Topic" };
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);

            return await response.DeserializeAsync<TopicBriefDto>(_output);
        }

        private async Task<TagDto> EnsureExistingTag(Guid topicId)
        {
            var route = CreateTag.BuildRoute(topicId);
            var request = new CreateTagRequest() { Name = "Tag", Colour = "#808080" };
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);

            return await response.DeserializeAsync<TagDto>(_output);
        }
    }
}
