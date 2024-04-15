using Cramming.SharedKernel;

namespace Cramming.UnitTests.SharedKernel
{
    public class BinaryContentTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var content = Array.Empty<byte>();
            var type = "Content Type";
            var name = "Content Name";

            // Act
            var binaryContent = new BinaryContent(content, type, name);

            // Assert
            binaryContent.Content.Should().BeEquivalentTo(content);
            binaryContent.Type.Should().Be(type);
            binaryContent.Name.Should().Be(name);
        }

        [Fact]
        public void Pdf_ShouldSetTypeAsPdf()
        {
            // Arrange
            var content = Array.Empty<byte>();
            var name = "Content Name";

            // Act
            var binaryContent = BinaryContent.Pdf(content, name);

            // Assert
            binaryContent.Content.Should().BeEquivalentTo(content);
            binaryContent.Type.Should().Be("application/pdf");
            binaryContent.Name.Should().Be(name);
        }
    }
}
