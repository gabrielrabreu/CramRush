using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.PDF;

namespace Cramming.IntegrationTests.PDF
{
    public class NotecardsPdfServiceTests
    {
        private readonly NotecardsPdfService _service;

        public NotecardsPdfServiceTests()
        {
            _service = new();
        }

        [Fact]
        public async Task GetsPdfDocumentForNotecards()
        {
            var topic = new Topic("Topic 1");
            topic.AddTag("Tag Name", string.Empty);
            topic.AddOpenEndedQuestion("Statement 1", "Answer 1");
            topic.AddMultipleChoiceQuestion("Statement 2").AddOption("Option 1", true);

            var document = await _service.ComposeAsync(topic);

            document.Should().NotBeNull();
            document.Content.Should().NotBeNull();
            document.ContentType.Should().Be("application/pdf");
            document.Name.Should().Contain("Topic_Notecards_");
        }
    }
}
