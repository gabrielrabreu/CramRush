using Cramming.Domain.Entities;
using FluentAssertions;

namespace Cramming.Domain.UnitTests.Entities
{
    public class TopicMultipleChoiceQuestionOptionEntityTests
    {
        [Fact]
        public void Constructor_WithValidArguments_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var statement = "Sample option statement";
            var isAnswer = true;

            // Act
            var optionEntity = new TopicMultipleChoiceQuestionOptionEntity(id, questionId, statement, isAnswer);

            // Assert
            optionEntity.Id.Should().Be(id);
            optionEntity.QuestionId.Should().Be(questionId);
            optionEntity.Statement.Should().Be(statement);
            optionEntity.IsAnswer.Should().Be(isAnswer);
        }

        [Fact]
        public void Constructor_WithoutId_ShouldGenerateNewId()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var statement = "Sample option statement";
            var isAnswer = true;

            // Act
            var optionEntity = new TopicMultipleChoiceQuestionOptionEntity(questionId, statement, isAnswer);

            // Assert
            optionEntity.Id.Should().NotBe(Guid.Empty);
            optionEntity.QuestionId.Should().Be(questionId);
            optionEntity.Statement.Should().Be(statement);
            optionEntity.IsAnswer.Should().Be(isAnswer);
        }
    }
}
