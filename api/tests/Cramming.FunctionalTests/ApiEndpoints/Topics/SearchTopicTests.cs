using Cramming.API.Topics;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class SearchTopicTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsOk()
        {
            await EnsureExistingTopic("Topic 1");
            var topic = await EnsureExistingTopic("Topic 2");
            await EnsureExistingTopic("Topic 3");

            var route = SearchTopic.BuildRoute(2, 1);
            var response = await _client.ExecuteGetAsync(route, _output);
            response.Should().NotBeNull().And.Subject.EnsureOK();

            var result = await response.DeserializeAsync<StaticPagedList<TopicBriefDto>>(_output);
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1)
                .And.SatisfyRespectively(item =>
                {
                    item.Id.Should().Be(topic.Id);
                    item.Name.Should().Be(topic.Name);
                });
            result.PageNumber.Should().Be(2);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(3);
            result.HasPreviousPage.Should().BeTrue();
            result.HasNextPage.Should().BeTrue();

        }

        private async Task<TopicBriefDto> EnsureExistingTopic(string name)
        {
            var route = CreateTopic.Route;
            var request = new CreateTopicRequest() { Name = name };
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);
            var result = await response.DeserializeAsync<TopicBriefDto>(_output);
            return result;
        }
    }
}
