using Cramming.Domain.TopicAggregate;
using Cramming.Infrastructure.Pdf;
using QuestPDF.Infrastructure;

namespace Cramming.IntegrationTests.Pdf
{
    public class PracticeTestPdfServiceTests
    {
        private readonly PracticeTestPdfService _service;

        public PracticeTestPdfServiceTests()
        {
            _service = new PracticeTestPdfService();
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
