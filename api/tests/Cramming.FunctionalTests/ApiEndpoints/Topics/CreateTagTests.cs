using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class CreateTagTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsCreatedGivenExistingTopicAndValidRequest()
        {
            var existingTopic = await EnsureExistingTopic();

            var route = CreateTag.BuildRoute(existingTopic.Id);
            var request = new CreateTagRequest() { Name = "New Tag", Colour = "#808080" };
            var content = request.FromModelAsJson();
            
            var response = await _client.ExecutePostAsync(route, content, _output);
            response.Should().NotBeNull().And.Subject.EnsureCreated();

            var result = await response.DeserializeAsync<TagDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be(request.Name);
            result.Colour.Should().Be(request.Colour);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTopic()
        {
            var route = CreateTag.BuildRoute(Guid.NewGuid());
            var request = new CreateTagRequest() { Name = "New Tag", Colour = "#808080" };
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);

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
    }
}
