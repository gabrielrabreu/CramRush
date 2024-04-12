using Cramming.Domain.TopicAggregate;

namespace Cramming.UnitTests.Domain.TopicAggregate
{
    public class TopicTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var name = "Sample Topic";

            // Act
            var topic = new Topic(name);

            // Assert
            topic.Name.Should().Be(name);
            topic.Tags.Should().BeEmpty();
            topic.Questions.Should().BeEmpty();
        }

        [Fact]
        public void UpdateName_ShouldUpdateName()
        {
            // Arrange
            var topic = new Topic("Old Name");
            var newName = "New Name";

            // Act
            topic.UpdateName(newName);

            // Assert
            topic.Name.Should().Be(newName);
        }

        [Fact]
        public void AddTag_ShouldAddTagToList()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagName = "Sample Tag";
            var tagColour = "#FF0000";

            // Act
            var tag = topic.AddTag(tagName, tagColour);

            // Assert
            tag.Should().NotBeNull();
            topic.Tags.Should().Contain(tag);
            tag.Name.Should().Be(tagName);
            tag.Colour.Code.Should().Be(tagColour);
        }

        [Fact]
        public void UpdateTagName_ShouldUpdateTagName()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagName = "Old Tag Name";
            var tag = new Tag(topic.Id, tagName);
            topic.Tags.Add(tag);
            var newTagName = "New Tag Name";

            // Act
            topic.UpdateTagName(tag.Id, newTagName);

            // Assert
            topic.Tags.First(t => t.Id == tag.Id).Name.Should().Be(newTagName);
        }

        [Fact]
        public void UpdateTagColour_ShouldUpdateTagColour()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagName = "Sample Tag";
            var tag = new Tag(topic.Id, tagName);
            topic.Tags.Add(tag);
            var newColour = "#00FF00";

            // Act
            topic.UpdateTagColour(tag.Id, newColour);

            // Assert
            topic.Tags.First(t => t.Id == tag.Id).Colour.Code.Should().Be(newColour);
        }

        [Fact]
        public void DeleteTag_ShouldDeleteTagFromList()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagName = "Sample Tag";
            var tag = new Tag(topic.Id, tagName);
            topic.Tags.Add(tag);

            // Act
            topic.DeleteTag(tag.Id);

            // Assert
            topic.Tags.Should().NotContain(tag);
        }

        [Fact]
        public void HasTag_ShouldReturnTrue_WhenTagExists()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagName = "Sample Tag";
            var tag = new Tag(topic.Id, tagName);
            topic.Tags.Add(tag);

            // Act
            var hasTag = topic.HasTag(tag.Id);

            // Assert
            hasTag.Should().BeTrue();
        }

        [Fact]
        public void HasTag_ShouldReturnFalse_WhenTagDoesNotExist()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagId = Guid.NewGuid();

            // Act
            var hasTag = topic.HasTag(tagId);

            // Assert
            hasTag.Should().BeFalse();
        }

        [Fact]
        public void HasTags_ShouldReturnTrue_WhenTagsExist()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagId = Guid.NewGuid();
            var tagName = "Sample Tag";
            var tag = new Tag(tagId, tagName);
            topic.Tags.Add(tag);

            // Act
            var hasTags = topic.HasTags();

            // Assert
            hasTags.Should().BeTrue();
        }

        [Fact]
        public void HasTags_ShouldReturnFalse_WhenNoTagsExist()
        {
            // Arrange
            var topic = new Topic("Sample Topic");

            // Act
            var hasTags = topic.HasTags();

            // Assert
            hasTags.Should().BeFalse();
        }

        [Fact]
        public void DoesNotHaveTag_ShouldReturnTrue_WhenTagDoesNotExist()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagId = Guid.NewGuid();

            // Act
            var doesNotHaveTag = topic.DoesNotHaveTag(tagId);

            // Assert
            doesNotHaveTag.Should().BeTrue();
        }

        [Fact]
        public void DoesNotHaveTag_ShouldReturnFalse_WhenTagExists()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var tagName = "Sample Tag";
            var tag = new Tag(topic.Id, tagName);
            topic.Tags.Add(tag);

            // Act
            var doesNotHaveTag = topic.DoesNotHaveTag(tag.Id);

            // Assert
            doesNotHaveTag.Should().BeFalse();
        }

        [Fact]
        public void AddOpenEndedQuestion_ShouldAddQuestionToList()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var statement = "Sample question statement";
            var answer = "Sample answer";

            // Act
            var question = topic.AddOpenEndedQuestion(statement, answer);

            // Assert
            topic.Questions.Should().Contain(question);
            question.Statement.Should().Be(statement);
        }

        [Fact]
        public void AddMultipleChoiceQuestion_ShouldAddQuestionToList()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            var statement = "Sample question statement";

            // Act
            var question = topic.AddMultipleChoiceQuestion(statement);

            // Assert
            topic.Questions.Should().Contain(question);
            question.Statement.Should().Be(statement);
        }

        [Fact]
        public void ClearQuestions_ShouldRemoveAllQuestionsFromList()
        {
            // Arrange
            var topic = new Topic("Sample Topic");
            topic.AddOpenEndedQuestion("Question 1", "Answer 1");
            topic.AddMultipleChoiceQuestion("Question 2");

            // Act
            topic.ClearQuestions();

            // Assert
            topic.Questions.Should().BeEmpty();
        }
    }
}
