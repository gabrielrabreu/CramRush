using Cramming.Domain.TopicAggregate;

namespace Cramming.UnitTests.Domain.TopicAggregate
{
    public class MultipleChoiceQuestionTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var statement = "Sample statement";

            // Act
            var question = new MultipleChoiceQuestion(topicId, statement);

            // Assert
            question.TopicId.Should().Be(topicId);
            question.Statement.Should().Be(statement);
        }

        [Fact]
        public void AddOption_ShouldAddOptionToList()
        {
            // Arrange
            var question = new MultipleChoiceQuestion(Guid.NewGuid(), "Sample statement");
            var optionStatement = "Option statement";
            var isAnswer = true;

            // Act
            var option = question.AddOption(optionStatement, isAnswer);

            // Assert
            option.Should().NotBeNull();
            option.Statement.Should().Be(optionStatement);
            option.IsAnswer.Should().Be(isAnswer);
            question.Options.Should().Contain(option);
        }
    }
}
