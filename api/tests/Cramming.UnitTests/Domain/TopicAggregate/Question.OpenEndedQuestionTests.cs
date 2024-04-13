using Cramming.Domain.TopicAggregate;

namespace Cramming.UnitTests.Domain.TopicAggregate
{
    public class OpenEndedQuestionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var statement = "Sample statement";
            var answer = "Sample answer";

            // Act
            var question = new OpenEndedQuestion(topicId, statement, answer);

            // Assert
            question.TopicId.Should().Be(topicId);
            question.Topic.Should().BeNull();
            question.Statement.Should().Be(statement);
            question.Answer.Should().Be(answer);
        }
    }
}
