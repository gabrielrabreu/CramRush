using Cramming.Domain.QuizAggregate;
using Cramming.Infrastructure.Pdf;

namespace Cramming.IntegrationTests.Pdf
{
    public class QuizFlashcardsPdfServiceTests
    {
        private readonly QuizFlashcardsPdfService _service;

        public QuizFlashcardsPdfServiceTests()
        {
            _service = new QuizFlashcardsPdfService();
            QuestPDF.Settings.License = LicenseType.Community;
        }

        [Fact]
        public void CreateReturnsBinaryContent()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");
            var quizQuestion = new QuizQuestion("Sample Statement");
            var quizQuestionOption = new QuizQuestionOption("Sample Text", true);
            quizQuestion.AddOption(quizQuestionOption);
            quiz.AddQuestion(quizQuestion);

            // Act
            var content = _service.Create(quiz);
            
            // Assert
            content.Should().NotBeNull();
        }
    }
}
