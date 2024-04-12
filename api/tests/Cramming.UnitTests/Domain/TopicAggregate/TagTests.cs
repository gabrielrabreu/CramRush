using Cramming.Domain.TopicAggregate;

namespace Cramming.UnitTests.Domain.TopicAggregate
{
    public class TagTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var topicId = Guid.NewGuid();
            var name = "Sample Tag";

            // Act
            var tag = new Tag(topicId, name);

            // Assert
            tag.TopicId.Should().Be(topicId);
            tag.Name.Should().Be(name);
            tag.Colour.Should().BeNull();
        }

        [Fact]
        public void UpdateName_ShouldUpdateName()
        {
            // Arrange
            var tag = new Tag(Guid.NewGuid(), "Old Name");
            var newName = "New Name";

            // Act
            tag.UpdateName(newName);

            // Assert
            tag.Name.Should().Be(newName);
        }

        [Fact]
        public void SetColour_ShouldSetColour()
        {
            // Arrange
            var tag = new Tag(Guid.NewGuid(), "Sample Tag");
            var newColour = new Colour("#00FF00");

            // Act
            tag.SetColour(newColour);

            // Assert
            tag.Colour.Should().Be(newColour);
        }
    }
}
