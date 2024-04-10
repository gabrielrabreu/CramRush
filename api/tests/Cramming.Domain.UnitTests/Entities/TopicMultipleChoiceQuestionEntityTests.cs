using Cramming.Domain.Entities;
using Cramming.Domain.ValueObjects;
using FluentAssertions;

namespace Cramming.Domain.UnitTests.Entities
{
    public class TopicMultipleChoiceQuestionEntityTests
    {
        [Fact]
        public void Constructor_WithValidArguments_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var topicId = Guid.NewGuid();
            var statement = "Sample question statement";
            var options = new List<TopicMultipleChoiceQuestionOptionEntity>();

            // Act
            var questionEntity = new TopicMultipleChoiceQuestionEntity(id, topicId, statement, options);

            // Assert
            questionEntity.Id.Should().Be(id);
            questionEntity.TopicId.Should().Be(topicId);
            questionEntity.Statement.Should().Be(statement);
            questionEntity.Options.Should().BeEquivalentTo(options);
        }

        [Fact]
        public void Constructor_WithoutId_ShouldGenerateNewId()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var statement = "Sample question statement";
            var parameters = new AssociateQuestionParameters
            {
                Statement = statement,
                Options = []
            };

            // Act
            var questionEntity = new TopicMultipleChoiceQuestionEntity(topicId, parameters);

            // Assert
            questionEntity.Id.Should().NotBe(Guid.Empty);
            questionEntity.TopicId.Should().Be(topicId);
            questionEntity.Statement.Should().Be(statement);
            questionEntity.Options.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_WithAssociateQuestionParameters_ShouldAssociateOptionsCorrectly()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var statement = "Sample question statement";
            var option1Statement = "Option 1";
            var option2Statement = "Option 2";
            var option3Statement = "Option 3";
            var parameters = new AssociateQuestionParameters
            {
                Statement = statement,
                Options =
                [
                    new() { Statement = option1Statement, IsAnswer = true },
                    new() { Statement = option2Statement, IsAnswer = false },
                    new() { Statement = option3Statement, IsAnswer = true }
                ]
            };

            // Act
            var questionEntity = new TopicMultipleChoiceQuestionEntity(topicId, parameters);

            // Assert
            questionEntity.Id.Should().NotBe(Guid.Empty);
            questionEntity.TopicId.Should().Be(topicId);
            questionEntity.Statement.Should().Be(statement);
            questionEntity.Options.Should().HaveCount(3);
            questionEntity.Options.Should().ContainSingle(o => o.Statement == option1Statement && o.IsAnswer);
            questionEntity.Options.Should().ContainSingle(o => o.Statement == option2Statement && !o.IsAnswer);
            questionEntity.Options.Should().ContainSingle(o => o.Statement == option3Statement && o.IsAnswer);
        }

        [Fact]
        public void AssociateOption_ShouldAddOptionToOptionsList()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var topicId = Guid.NewGuid();
            var statement = "Sample question statement";
            var questionEntity = new TopicMultipleChoiceQuestionEntity(questionId, topicId, statement, []);
            var optionStatement = "Option 1";
            var isAnswer = true;

            // Act
            var optionEntity = questionEntity.AssociateOption(optionStatement, isAnswer);

            // Assert
            optionEntity.Should().NotBeNull();
            optionEntity.Statement.Should().Be(optionStatement);
            optionEntity.IsAnswer.Should().Be(isAnswer);
            questionEntity.Options.Should().Contain(optionEntity);
        }
    }
}
