using Cramming.Domain.QuizAggregate;

namespace Cramming.UnitTests.Domain.QuizAggregate
{
    public class QuizQuestionOptionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var text = "Sample Text";
            var isCorrect = true;

            // Act
            var option = new QuizQuestionOption(text, isCorrect);

            // Assert
            option.Text.Should().Be(text);
            option.IsCorrect.Should().Be(isCorrect);
            option.Question.Should().BeNull();
            option.QuestionId.Should().BeEmpty();
        }

        [Fact]
        public void SetQuestionId_ShouldUpdateQuestionId()
        {
            // Arrange
            var option = new QuizQuestionOption("Sample Text", true);
            var newQuestionId = Guid.NewGuid();

            // Act
            option.SetQuestionId(newQuestionId);

            // Assert
            option.QuestionId.Should().Be(newQuestionId);
        }
    }
}
