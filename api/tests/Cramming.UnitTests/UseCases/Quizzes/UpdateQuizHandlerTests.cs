using Cramming.Domain.QuizAggregate;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.Update;

namespace Cramming.UnitTests.UseCases.Quizzes
{
    public class UpdateQuizHandlerTests
    {
        private readonly Mock<IQuizRepository> _repositoryMock;
        private readonly UpdateQuizHandler _handler;

        public UpdateQuizHandlerTests()
        {
            _repositoryMock = new Mock<IQuizRepository>();
            _handler = new UpdateQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenQuizFound_ShouldUpdateQuiz()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");

            var request = new UpdateQuizCommand(
                quiz.Id,
                "New Title",
                [
                    new UpdateQuizCommand.QuestionDto(
                        "Sample Statement",
                        [ new UpdateQuizCommand.QuestionOptionDto("Sample Text", true) ])
                ]);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            quiz.Title.Should().Be(request.Title);

            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Quiz>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.UpdateAsync(quiz, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new UpdateQuizCommand(Guid.NewGuid(), "New Title", []);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Quiz>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
