using Cramming.Domain.QuizAttemptAggregate;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.DownloadToReply;

namespace Cramming.UnitTests.UseCases.QuizAttempts
{
    public class DownloadQuizAttemptToReplyHandlerTests
    {
        private readonly Mock<IQuizAttemptReadRepository> _repositoryMock;
        private readonly Mock<IQuizAttemptToReplyPdfService> _serviceMock;
        private readonly DownloadQuizAttemptToReplyHandler _handler;

        public DownloadQuizAttemptToReplyHandlerTests()
        {
            _repositoryMock = new Mock<IQuizAttemptReadRepository>();
            _serviceMock = new Mock<IQuizAttemptToReplyPdfService>();
            _handler = new DownloadQuizAttemptToReplyHandler(_repositoryMock.Object, _serviceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenQuizAttemptFound_ShouldReturnBinaryContent()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");

            var request = new DownloadQuizAttemptToReplyQuery(quizAttempt.Id);
            var cancellationToken = new CancellationToken();

            var binaryContent = new BinaryContent([], "Binary Type", "Test");

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            _serviceMock.Setup(mock => mock.Create(It.IsAny<QuizAttempt>()))
                .Returns(binaryContent);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizAttemptId, cancellationToken), Times.Once);

            _serviceMock.Verify(mock => mock.Create(It.IsAny<QuizAttempt>()), Times.Once);
            _serviceMock.Verify(mock => mock.Create(quizAttempt), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new DownloadQuizAttemptToReplyQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizAttemptId, cancellationToken), Times.Once);

            _serviceMock.Verify(mock => mock.Create(It.IsAny<QuizAttempt>()), Times.Never);
        }
    }
}
