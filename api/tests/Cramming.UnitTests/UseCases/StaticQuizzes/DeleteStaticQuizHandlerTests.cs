using Cramming.Domain.StaticQuizAggregate;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Delete;

namespace Cramming.UnitTests.UseCases.StaticQuizzes
{
    public class DeleteStaticQuizHandlerTests
    {
        private readonly Mock<IStaticQuizRepository> _repositoryMock;
        private readonly DeleteStaticQuizHandler _handler;

        public DeleteStaticQuizHandlerTests()
        {
            _repositoryMock = new Mock<IStaticQuizRepository>();
            _handler = new DeleteStaticQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFoundStaticQuiz_ShouldDeleteStaticQuiz()
        {
            // Arrange
            var quiz = new StaticQuiz("Sample Title");

            var request = new DeleteStaticQuizCommand(quiz.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.StaticQuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<StaticQuiz>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.DeleteAsync(quiz, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenStaticQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new DeleteStaticQuizCommand(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.StaticQuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<StaticQuiz>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
