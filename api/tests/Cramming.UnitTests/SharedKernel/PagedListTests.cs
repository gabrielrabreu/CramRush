using Cramming.SharedKernel;

namespace Cramming.UnitTests.SharedKernel
{
    public class PagedListTests
    {
        [Fact]
        public void Constructor_ShouldSetProperties()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var count = 15;
            var pageNumber = 2;
            var pageSize = 5;

            // Act
            var pagedList = new PagedList<int>(items, count, pageNumber, pageSize);

            // Assert
            pagedList.Items.Should().Equal(items);
            pagedList.PageNumber.Should().Be(pageNumber);
            pagedList.TotalPages.Should().Be(3);
            pagedList.TotalCount.Should().Be(count);
            pagedList.HasPreviousPage.Should().BeTrue();
            pagedList.HasNextPage.Should().BeTrue();
        }

        [Fact]
        public void HasPreviousPage_WhenPageNumberGreaterThan1_ShouldReturnsTrue()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var count = 15;
            var pageNumber = 2;
            var pageSize = 5;

            // Act
            var pagedList = new PagedList<int>(items, count, pageNumber, pageSize);

            // Assert
            pagedList.HasPreviousPage.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void HasPreviousPage_WhenPageNumberLessThanOrEqualTo1_ShouldReturnsFalse(int pageNumber)
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var count = 15;
            var pageSize = 5;

            // Act
            var pagedList = new PagedList<int>(items, count, pageNumber, pageSize);

            // Assert
            pagedList.HasPreviousPage.Should().BeFalse();
        }

        [Fact]
        public void HasNextPage_WhenPageNumberLessThanTotalPages_ReturnsTrue()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var count = 15;
            var pageNumber = 2;
            var pageSize = 5;

            // Act
            var pagedList = new PagedList<int>(items, count, pageNumber, pageSize);

            // Assert
            pagedList.HasNextPage.Should().BeTrue();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        public void HasNextPage_WhenPageNumberGreaterThanOrEqualToTotalPages_ReturnsFalse(int pageNumber)
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var count = 15;
            var pageSize = 5;

            // Act
            var pagedList = new PagedList<int>(items, count, pageNumber, pageSize);

            // Assert
            pagedList.HasNextPage.Should().BeFalse();
        }
    }
}
