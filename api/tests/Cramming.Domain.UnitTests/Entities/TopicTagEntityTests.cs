using Cramming.Domain.Entities;
using FluentAssertions;

namespace Cramming.Domain.UnitTests.Entities
{
    public class TopicTagEntityTests
    {
        [Fact]
        public void Constructor_WithValidArguments_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var topicId = Guid.NewGuid();
            var name = "Tag Name";
            var colour = "#FF0000";

            // Act
            var tagEntity = new TopicTagEntity(id, topicId, name, colour);

            // Assert
            tagEntity.Id.Should().Be(id);
            tagEntity.TopicId.Should().Be(topicId);
            tagEntity.Name.Should().Be(name);
            tagEntity.Colour.Code.Should().Be(colour);
        }

        [Fact]
        public void Constructor_WithoutId_ShouldGenerateNewId()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var name = "Tag Name";
            var colour = "#FF0000";

            // Act
            var tagEntity = new TopicTagEntity(topicId, name, colour);

            // Assert
            tagEntity.Id.Should().NotBe(Guid.Empty);
            tagEntity.TopicId.Should().Be(topicId);
            tagEntity.Name.Should().Be(name);
            tagEntity.Colour.Code.Should().Be(colour);
        }
    }
}
