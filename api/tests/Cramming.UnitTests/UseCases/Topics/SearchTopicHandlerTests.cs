using Cramming.SharedKernel;
using Cramming.UseCases.Topics;
using Cramming.UseCases.Topics.Search;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class SearchTopicHandlerTests
    {
        private readonly Mock<ISearchTopicQueryService> _serviceMock;
        private readonly SearchTopicHandler _handler;

        public SearchTopicHandlerTests()
        {
            _serviceMock = new Mock<ISearchTopicQueryService>();
            _handler = new SearchTopicHandler(_serviceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPàgedList()
        {
            // Arrange
            var pagedList = new PagedList<TopicBriefDTO>([], 0, 1, 2);

            var request = new SearchTopicQuery(1, 2);
            var cancellationToken = new CancellationToken();

            _serviceMock.Setup(mock => mock.SearchAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Should().Be(pagedList);

            _serviceMock.Verify(mock => mock.SearchAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _serviceMock.Verify(mock => mock.SearchAsync(request.PageNumber, request.PageSize, cancellationToken), Times.Once);
        }
    }
}
