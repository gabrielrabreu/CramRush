using Cramming.Domain.StaticQuizAggregate;

namespace Cramming.UnitTests.Domain.StaticQuizAggregate
{
    public class StaticQuizQuestionOptionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var text = "Sample Text";
            var isCorrect = true;

            // Act
            var option = new StaticQuizQuestionOption(text, isCorrect);

            // Assert
            option.Text.Should().Be(text);
            option.IsCorrect.Should().Be(isCorrect);
        }

        [Fact]
        public void SetQuestionId_ShouldUpdateQuestionId()
        {
            // Arrange
            var option = new StaticQuizQuestionOption("Sample Text", true);
            var newQuestionId = Guid.NewGuid();

            // Act
            option.SetQuestionId(newQuestionId);

            // Assert
            option.QuestionId.Should().Be(newQuestionId);
        }
    }
}
