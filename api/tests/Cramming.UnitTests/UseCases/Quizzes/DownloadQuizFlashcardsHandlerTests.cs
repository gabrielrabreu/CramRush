using Cramming.Domain.QuizAggregate;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.DownloadFlashcards;

namespace Cramming.UnitTests.UseCases.Quizzes
{
    public class DownloadQuizFlashcardsHandlerTests
    {
        private readonly Mock<IQuizReadRepository> _repositoryMock;
        private readonly Mock<IQuizFlashcardsPdfService> _serviceMock;
        private readonly DownloadQuizFlashcardsHandler _handler;

        public DownloadQuizFlashcardsHandlerTests()
        {
            _repositoryMock = new Mock<IQuizReadRepository>();
            _serviceMock = new Mock<IQuizFlashcardsPdfService>();
            _handler = new DownloadQuizFlashcardsHandler(_repositoryMock.Object, _serviceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenQuizFound_ShouldReturnBinaryContent()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");
            
            var request = new DownloadQuizFlashcardsQuery(quiz.Id);
            var cancellationToken = new CancellationToken();

            var binaryContent = new BinaryContent([], "Binary Type", "Test");

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            _serviceMock.Setup(mock => mock.Create(It.IsAny<Quiz>()))
                .Returns(binaryContent);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);

            _serviceMock.Verify(mock => mock.Create(It.IsAny<Quiz>()), Times.Once);
            _serviceMock.Verify(mock => mock.Create(quiz), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new DownloadQuizFlashcardsQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);

            _serviceMock.Verify(mock => mock.Create(It.IsAny<Quiz>()), Times.Never);
        }
    }
}
