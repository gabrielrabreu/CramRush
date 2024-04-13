using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class CreateTopicTests(CustomWebApplicationFactory factory, ITestOutputHelper output) 
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsCreatedGivenValidRequest()
        {
            var route = CreateTopic.Route;
            var request = new CreateTopicRequest() { Name = "New Topic" };
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);
            response.Should().NotBeNull().And.Subject.EnsureCreated();

            var result = await response.DeserializeAsync<TopicBriefDto>(_output);
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be(request.Name);
            response.EnsureLocation(GetTopicById.BuildRoute(result.Id), _output);
        }
    }
}
