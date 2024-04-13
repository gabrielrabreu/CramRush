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
    }
}
