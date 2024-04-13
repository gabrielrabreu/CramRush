using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Pdf;
using QuestPDF.Infrastructure;

namespace Cramming.IntegrationTests.Pdf
{
    public class NotecardsPdfServiceTests
    {
        private readonly NotecardsPdfService _service;

        public NotecardsPdfServiceTests()
        {
            _service = new NotecardsPdfService();
            QuestPDF.Settings.License = LicenseType.Community;
        }

        [Fact]
        public void CreateReturnsBinaryContent()
        {
            var model = new Topic("Topic Name");
            var content = _service.Create(model);
            content.Should().NotBeNull();
        }

        [Fact]
        public void CreateWithTagsReturnsBinaryContent()
        {
            var model = new Topic("Topic");
            model.AddTag("Tag 1", "#808080");
            model.AddTag("Tag 2", string.Empty);
            var content = _service.Create(model);
            content.Should().NotBeNull();
        }

        [Fact]
        public void CreateWithOpenEndedQuestionReturnsBinaryContent()
        {
            var model = new Topic("Topic Name");
            model.AddOpenEndedQuestion("Statement 1", "Answer 1");
            model.AddOpenEndedQuestion("Statement 2", "Answer 2");
            model.AddOpenEndedQuestion("Statement 3", "Answer 3");
            var content = _service.Create(model);
            content.Should().NotBeNull();
        }

        [Fact]
        public void CreateWithMultipleChoiceQuestionReturnsBinaryContent()
        {
            var model = new Topic("Topic Name");
            model.AddMultipleChoiceQuestion("Statement 1").AddOption("Option 1", true);
            model.AddMultipleChoiceQuestion("Statement 2").AddOption("Option 2.1", false);
            model.AddMultipleChoiceQuestion("Statement 3");
            var content = _service.Create(model);
            content.Should().NotBeNull();
        }
    }
}
