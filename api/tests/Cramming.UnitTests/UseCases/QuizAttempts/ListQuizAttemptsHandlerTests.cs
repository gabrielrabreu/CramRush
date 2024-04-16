using Cramming.UseCases.QuizAttempts.List;

namespace Cramming.UnitTests.UseCases.QuizAttempts
{
    public class ListQuizAttemptsHandlerTests
    {
        private readonly Mock<IListQuizAttemptsService> _serviceMock;
        private readonly ListQuizAttemptsHandler _handler;

        public ListQuizAttemptsHandlerTests()
        {
            _serviceMock = new Mock<IListQuizAttemptsService>();
            _handler = new ListQuizAttemptsHandler(_serviceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPagedList()
        {
            // Arrange
            var pagedList = new PagedList<QuizAttemptBriefDto>([], 0, 1, 2);

            var request = new ListQuizAttemptsQuery(1, 2);
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
