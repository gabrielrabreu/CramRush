using Cramming.Domain.StaticQuizAggregate;

namespace Cramming.UnitTests.Domain.StaticQuizAggregate
{
    public class StaticQuizQuestionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var statement = "Sample Statement";

            // Act
            var question = new StaticQuizQuestion(statement);

            // Assert
            question.Statement.Should().Be(statement);
            question.Options.Should().BeEmpty();
            question.Quiz.Should().BeNull();
            question.QuizId.Should().BeEmpty();
        }

        [Fact]
        public void SetQuizId_ShouldUpdateQuizId()
        {
            // Arrange
            var question = new StaticQuizQuestion("Sample Statement");
            var newQuizId = Guid.NewGuid();

            // Act
            question.SetQuizId(newQuizId);

            // Assert
            question.QuizId.Should().Be(newQuizId);
        }

        [Fact]
        public void AddOption_ShouldAddOptionToList()
        {
            // Arrange
            var question = new StaticQuizQuestion("Sample Statement");
            var option = new StaticQuizQuestionOption("Sample Text", true);

            // Act
            question.AddOption(option);

            // Assert
            question.Options.Should().Contain(option);
        }
    }
}
