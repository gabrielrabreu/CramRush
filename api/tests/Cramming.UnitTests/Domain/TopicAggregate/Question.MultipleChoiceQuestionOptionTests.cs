using Cramming.Domain.TopicAggregate;

namespace Cramming.UnitTests.Domain.TopicAggregate
{
    public class MultipleChoiceQuestionOptionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var statement = "Sample statement";
            var isAnswer = true;

            // Act
            var option = new MultipleChoiceQuestionOption(questionId, statement, isAnswer);

            // Assert
            option.QuestionId.Should().Be(questionId);
            option.Statement.Should().Be(statement);
            option.IsAnswer.Should().Be(isAnswer);
        }
    }
}
