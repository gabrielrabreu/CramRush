using Cramming.Domain.TopicAggregate;

namespace Cramming.UnitTests.Domain.TopicAggregate
{
    public class ColourTests
    {
        [Fact]
        public void Constructor_WithNullCode_ShouldSetCodeToDefault()
        {
            // Arrange & Act
            var colour = new Colour(null);

            // Assert
            colour.Code.Should().Be("#FFFFFF");
        }

        [Fact]
        public void Constructor_WithEmptyCode_ShouldSetCodeToDefault()
        {
            // Arrange & Act
            var colour = new Colour("");

            // Assert
            colour.Code.Should().Be("#FFFFFF");
        }

        [Fact]
        public void Constructor_WithNonEmptyCode_ShouldSetCode()
        {
            // Arrange
            var code = "#FF0000";

            // Act
            var colour = new Colour(code);

            // Assert
            colour.Code.Should().Be(code);
        }

        [Fact]
        public void Equals_WithSameCode_ShouldReturnTrue()
        {
            // Arrange
            var code = "#FF0000";
            var colour1 = new Colour(code);
            var colour2 = new Colour(code);

            // Act & Assert
            colour1.Equals(colour2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentCode_ShouldReturnFalse()
        {
            // Arrange
            var colour1 = new Colour("#FF0000");
            var colour2 = new Colour("#00FF00");

            // Act & Assert
            colour1.Equals(colour2).Should().BeFalse();
        }
    }
}
