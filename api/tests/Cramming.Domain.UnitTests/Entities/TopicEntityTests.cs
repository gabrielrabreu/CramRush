using Cramming.Domain.Entities;
using Cramming.Domain.Enums;
using Cramming.Domain.ValueObjects;
using FluentAssertions;

namespace Cramming.Domain.UnitTests.Entities
{
    public class TopicEntityTests
    {
        [Fact]
        public void Constructor_WithValidArguments_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Sample topic";
            var description = "Sample topic description";
            var tags = new List<TopicTagEntity>();
            var questions = new List<TopicQuestionEntity>();

            // Act
            var topicEntity = new TopicEntity(id, name, description, tags, questions);

            // Assert
            topicEntity.Id.Should().Be(id);
            topicEntity.Name.Should().Be(name);
            topicEntity.Description.Should().Be(description);
            topicEntity.Tags.Should().BeEquivalentTo(tags);
            topicEntity.Questions.Should().BeEquivalentTo(questions);
        }

        [Fact]
        public void Constructor_WithoutId_ShouldGenerateNewId()
        {
            // Arrange
            var name = "Sample topic";
            var description = "Sample topic description";

            // Act
            var topicEntity = new TopicEntity(name, description);

            // Assert
            topicEntity.Id.Should().NotBe(Guid.Empty);
            topicEntity.Name.Should().Be(name);
            topicEntity.Description.Should().Be(description);
            topicEntity.Tags.Should().BeEmpty();
            topicEntity.Questions.Should().BeEmpty();
        }

        [Fact]
        public void AssociateTag_ShouldAddTagToTagsList()
        {
            // Arrange
            var tagName = "Sample tag";
            var tagColour = "#FF0000";
            var topicEntity = new TopicEntity("Sample topic", "Sample topic description");

            // Act
            var tagEntity = topicEntity.AssociateTag(tagName, tagColour);

            // Assert
            tagEntity.Should().NotBeNull();
            tagEntity.Name.Should().Be(tagName);
            tagEntity.Colour.Code.Should().Be(tagColour);
            topicEntity.Tags.Should().Contain(tagEntity);
        }

        [Fact]
        public void UpdateTag_ShouldUpdateTagProperties()
        {
            // Arrange
            var tagName = "Sample tag";
            var tagColour = "#FF0000";
            var topicEntity = new TopicEntity("Sample topic", "Sample topic description");
            var tagEntity = topicEntity.AssociateTag(tagName, tagColour);

            // Act
            var updatedTagName = "Updated tag";
            var updatedTagColour = "#00FF00";
            topicEntity.UpdateTag(tagEntity.Id, updatedTagName, updatedTagColour);

            // Assert
            tagEntity.Name.Should().Be(updatedTagName);
            tagEntity.Colour.Code.Should().Be(updatedTagColour);
        }

        [Fact]
        public void DisassociateTag_ShouldRemoveTagFromTagsList()
        {
            // Arrange
            var tagName = "Sample tag";
            var tagColour = "#FF0000";
            var topicEntity = new TopicEntity("Sample topic", "Sample topic description");
            var tagEntity = topicEntity.AssociateTag(tagName, tagColour);

            // Act
            topicEntity.DisassociateTag(tagEntity.Id);

            // Assert
            topicEntity.Tags.Should().NotContain(tagEntity);
        }

        [Fact]
        public void AssociateQuestion_WithOpenEnded_ShouldAddQuestionToQuestionsList()
        {
            // Arrange
            var openEndedStatement = "Sample open-ended question";
            var openEndedAnswer = "Sample open-ended answer";
            var parameters = new AssociateQuestionParameters
            {
                Type = QuestionType.OpenEnded,
                Statement = openEndedStatement,
                Answer = openEndedAnswer
            };
            var topicEntity = new TopicEntity("Sample topic", "Sample topic description");

            // Act
            var questionEntity = (TopicOpenEndedQuestionEntity)topicEntity.AssociateQuestion(parameters);

            // Assert
            questionEntity.Should().NotBeNull();
            questionEntity.Statement.Should().Be(openEndedStatement);
            questionEntity.Answer.Should().Be(openEndedAnswer);
            topicEntity.Questions.Should().Contain(questionEntity);
        }

        [Fact]
        public void AssociateQuestion_WithMultipleChoice_ShouldAddQuestionToQuestionsList()
        {
            // Arrange
            var multipleChoiceStatement = "Sample multiple-choice question";
            var parameters = new AssociateQuestionParameters
            {
                Type = QuestionType.MultipleChoice,
                Statement = multipleChoiceStatement,
                Options =
                [
                    new() { Statement = "Option 1", IsAnswer = true },
                    new() { Statement = "Option 2", IsAnswer = false }
                ]
            };
            var topicEntity = new TopicEntity("Sample topic", "Sample topic description");

            // Act
            var questionEntity = (TopicMultipleChoiceQuestionEntity)topicEntity.AssociateQuestion(parameters);

            // Assert
            questionEntity.Should().NotBeNull();
            questionEntity.Statement.Should().Be(multipleChoiceStatement);
            questionEntity.Options.Should().HaveCount(2);
            topicEntity.Questions.Should().Contain(questionEntity);
        }

        [Fact]
        public void ClearQuestions_ShouldRemoveAllQuestionsFromQuestionsList()
        {
            // Arrange
            var openEndedParameters = new AssociateQuestionParameters
            {
                Type = QuestionType.OpenEnded,
                Statement = "Sample open-ended question",
                Answer = "Sample answer"
            };
            var multipleChoiceParameters = new AssociateQuestionParameters
            {
                Type = QuestionType.MultipleChoice,
                Statement = "Sample multiple-choice question",
                Options =
                [
                    new() { Statement = "Option 1", IsAnswer = true },
                    new() { Statement = "Option 2", IsAnswer = false }
                ]
            };
            var topicEntity = new TopicEntity("Sample topic", "Sample topic description");
            topicEntity.AssociateQuestion(openEndedParameters);
            topicEntity.AssociateQuestion(multipleChoiceParameters);

            // Act
            topicEntity.ClearQuestions();

            // Assert
            topicEntity.Questions.Should().BeEmpty();
        }
    }
}
