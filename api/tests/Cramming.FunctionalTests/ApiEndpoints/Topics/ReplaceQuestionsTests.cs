using Cramming.API.Topics;
using Cramming.Domain.TopicAggregate;
using Cramming.FunctionalTests.Support;
using Cramming.UseCases.Topics;

namespace Cramming.FunctionalTests.ApiEndpoints.Topics
{
    public class ReplaceQuestionsTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task ReturnsNoContentGivenExistingTopicAndValidRequest()
        {
            var existingTopic = await EnsureExistingTopic();

            var route = ReplaceQuestions.BuildRoute(existingTopic.Id);
            var request = new ReplaceQuestionsRequest() { Questions = [
                new ReplaceQuestionsRequest.CreateQuestion() {
                    Type = QuestionType.OpenEnded,
                    Statement = "Statement 1",
                    Answer = "Answer 1",
                    Options = []
                }]};
            var content = request.FromModelAsJson();

            var response = await _client.ExecutePostAsync(route, content, _output);
            response.Should().NotBeNull().And.Subject.EnsureNoContent();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistingTopic()
        {
            var route = ReplaceQuestions.BuildRoute(Guid.NewGuid());
            var request = new ReplaceQuestionsRequest() { Questions = [] };
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
            var result = await response.DeserializeAsync<TopicBriefDto>(_output);
            return result;
        }
    }
}
