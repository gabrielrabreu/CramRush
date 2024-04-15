using Cramming.SharedKernel;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.List;

namespace Cramming.UnitTests.UseCases.StaticQuizzes
{
    public class ListStaticQuizzesHandlerTests
    {
        private readonly Mock<IListStaticQuizzesService> _serviceMock;
        private readonly ListStaticQuizzesHandler _handler;

        public ListStaticQuizzesHandlerTests()
        {
            _serviceMock = new Mock<IListStaticQuizzesService>();
            _handler = new ListStaticQuizzesHandler(_serviceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPagedList()
        {
            // Arrange
            var pagedList = new PagedList<StaticQuizBriefDto>([], 0, 1, 2);

            var request = new ListStaticQuizzesQuery(1, 2);
            var cancellationToken = new CancellationToken();

            _serviceMock.Setup(mock => mock.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Should().Be(pagedList);

            _serviceMock.Verify(mock => mock.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            _serviceMock.Verify(mock => mock.ListAsync(request.PageNumber, request.PageSize, cancellationToken), Times.Once);
        }
    }
}
