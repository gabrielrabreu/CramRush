using Cramming.Domain.ValueObjects;
using FluentAssertions;

namespace Cramming.Domain.UnitTests.ValueObjects
{
    public class ColourTests
    {
        [Fact]
        public void Constructor_WithValidCode_ShouldSetCode()
        {
            // Arrange
            string validCode = "#FF0000";

            // Act
            var colour = new Colour(validCode);

            // Assert
            colour.Code.Should().Be(validCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithNullOrWhiteSpaceCode_ShouldSetDefaultCode(string invalidCode)
        {
            // Act
            var colour = new Colour(invalidCode);

            // Assert
            colour.Code.Should().Be("#FFFFFF");
        }

        [Fact]
        public void Equal_ShouldBeEqual()
        {
            // Arrange
            var colour1 = new Colour("#FF0000");
            var colour2 = new Colour("#FF0000");

            // Act & Assert
            colour1.Equals(colour2).Should().BeTrue();
        }

        [Fact]
        public void Equal_ShouldNotBeEqual()
        {
            // Arrange
            var colour1 = new Colour("#FF0000");
            var colour2 = new Colour("#FF1111");

            // Act & Assert
            colour1.Equals(colour2).Should().BeFalse();
        }
    }
}
