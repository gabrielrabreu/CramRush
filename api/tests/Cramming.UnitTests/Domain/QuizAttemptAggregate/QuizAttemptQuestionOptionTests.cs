using Cramming.Domain.QuizAttemptAggregate;

namespace Cramming.UnitTests.Domain.QuizAttemptAggregate
{
    public class QuizAttemptQuestionOptionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var text = "Sample Text";
            var isCorrect = true;

            // Act
            var option = new QuizAttemptQuestionOption(text, isCorrect);

            // Assert
            option.Text.Should().Be(text);
            option.IsCorrect.Should().Be(isCorrect);
            option.Question.Should().BeNull();
            option.QuestionId.Should().BeEmpty();
        }

        [Fact]
        public void SetQuestionId_ShouldSetQuestionId()
        {
            // Arrange
            var option = new QuizAttemptQuestionOption("Sample Text", true);
            var newQuestionId = Guid.NewGuid();

            // Act
            option.SetQuestionId(newQuestionId);

            // Assert
            option.QuestionId.Should().Be(newQuestionId);
        }

        [Fact]
        public void MarkAsSelected_ShouldSetIsSelectedToTrue()
        {
            // Arrange
            var option = new QuizAttemptQuestionOption("Sample Text", true);

            // Act
            option.MarkAsSelected();

            // Assert
            option.IsSelected.Should().BeTrue();
        }
    }
}
