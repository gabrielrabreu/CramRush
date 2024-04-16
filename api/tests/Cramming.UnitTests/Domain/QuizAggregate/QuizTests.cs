using Cramming.Domain.QuizAggregate;

namespace Cramming.UnitTests.Domain.QuizAggregate
{
    public class QuizTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var title = "Sample Title";

            // Act
            var quiz = new Quiz(title);

            // Assert
            quiz.Title.Should().Be(title);
            quiz.Questions.Should().BeEmpty();
        }

        [Fact]
        public void SetTitle_ShouldUpdateTitle()
        {
            // Arrange
            var quiz = new Quiz("Old Title");
            var newTitle = "New Title";

            // Act
            quiz.SetTitle(newTitle);

            // Assert
            quiz.Title.Should().Be(newTitle);
        }

        [Fact]
        public void AddQuestion_ShouldAddQuestionToList()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");
            var question = new QuizQuestion("Sample Statement");

            // Act
            quiz.AddQuestion(question);

            // Assert
            quiz.Questions.Should().Contain(question);
        }

        [Fact]
        public void ClearQuestions_ShouldRemoveAllQuestionsFromList()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");
            quiz.AddQuestion(new QuizQuestion("Sample Statement"));

            // Act
            quiz.ClearQuestions();

            // Assert
            quiz.Questions.Should().BeEmpty();
        }
    }
}
