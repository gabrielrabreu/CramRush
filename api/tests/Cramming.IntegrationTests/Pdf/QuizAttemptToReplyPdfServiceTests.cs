using Cramming.Domain.QuizAttemptAggregate;
using Cramming.Infrastructure.Pdf;

namespace Cramming.IntegrationTests.Pdf
{
    public class QuizAttemptToReplyPdfServiceTests
    {
        private readonly QuizAttemptToReplyPdfService _service;

        public QuizAttemptToReplyPdfServiceTests()
        {
            _service = new QuizAttemptToReplyPdfService();
            QuestPDF.Settings.License = LicenseType.Community;
        }

        [Fact]
        public void CreateReturnsBinaryContent()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);

            // Act
            var content = _service.Create(quizAttempt);

            // Assert
            content.Should().NotBeNull();
        }
    }
}
