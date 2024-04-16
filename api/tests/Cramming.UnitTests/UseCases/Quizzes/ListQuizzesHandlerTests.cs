using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.List;

namespace Cramming.UnitTests.UseCases.Quizzes
{
    public class ListQuizzesHandlerTests
    {
        private readonly Mock<IListQuizzesService> _serviceMock;
        private readonly ListQuizzesHandler _handler;

        public ListQuizzesHandlerTests()
        {
            _serviceMock = new Mock<IListQuizzesService>();
            _handler = new ListQuizzesHandler(_serviceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPagedList()
        {
            // Arrange
            var pagedList = new PagedList<QuizBriefDto>([], 0, 1, 2);

            var request = new ListQuizzesQuery(1, 2);
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
