using Cramming.Domain.QuizAggregate;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.Delete;

namespace Cramming.UnitTests.UseCases.Quizzes
{
    public class DeleteQuizHandlerTests
    {
        private readonly Mock<IQuizRepository> _repositoryMock;
        private readonly DeleteQuizHandler _handler;

        public DeleteQuizHandlerTests()
        {
            _repositoryMock = new Mock<IQuizRepository>();
            _handler = new DeleteQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFoundQuiz_ShouldDeleteQuiz()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");

            var request = new DeleteQuizCommand(quiz.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<Quiz>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.DeleteAsync(quiz, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new DeleteQuizCommand(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<Quiz>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
