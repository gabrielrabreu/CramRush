using Cramming.Domain.Entities;
using Cramming.Domain.ValueObjects;
using FluentAssertions;

namespace Cramming.Domain.UnitTests.Entities
{
    public class TopicOpenEndedQuestionEntityTests
    {
        [Fact]
        public void Constructor_WithValidArguments_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var topicId = Guid.NewGuid();
            var statement = "Sample question statement";
            var answer = "Sample answer";

            // Act
            var questionEntity = new TopicOpenEndedQuestionEntity(id, topicId, statement, answer);

            // Assert
            questionEntity.Id.Should().Be(id);
            questionEntity.TopicId.Should().Be(topicId);
            questionEntity.Statement.Should().Be(statement);
            questionEntity.Answer.Should().Be(answer);
        }

        [Fact]
        public void Constructor_WithoutId_ShouldGenerateNewId()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var statement = "Sample question statement";
            var answer = "Sample answer";

            var parameters = new AssociateQuestionParameters
            {
                Statement = statement,
                Answer = answer
            };

            // Act
            var questionEntity = new TopicOpenEndedQuestionEntity(topicId, parameters);

            // Assert
            questionEntity.Id.Should().NotBe(Guid.Empty);
            questionEntity.TopicId.Should().Be(topicId);
            questionEntity.Statement.Should().Be(statement);
            questionEntity.Answer.Should().Be(answer);
        }
    }
}
