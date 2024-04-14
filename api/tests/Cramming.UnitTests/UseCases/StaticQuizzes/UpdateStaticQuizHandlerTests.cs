using Cramming.Domain.StaticQuizAggregate;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Update;

namespace Cramming.UnitTests.UseCases.StaticQuizzes
{
    public class UpdateStaticQuizHandlerTests
    {
        private readonly Mock<IStaticQuizRepository> _repositoryMock;
        private readonly UpdateStaticQuizHandler _handler;

        public UpdateStaticQuizHandlerTests()
        {
            _repositoryMock = new Mock<IStaticQuizRepository>();
            _handler = new UpdateStaticQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenStaticQuizFound_ShouldUpdateStaticQuiz()
        {
            // Arrange
            var quiz = new StaticQuiz("Sample Title");

            var request = new UpdateStaticQuizCommand(
                quiz.Id, 
                "New Title",
                [
                    new UpdateStaticQuizCommand.QuestionDto(
                        "Sample Statement",
                        [ new UpdateStaticQuizCommand.QuestionOptionDto("Sample Text", true) ])
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
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.StaticQuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<StaticQuiz>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.UpdateAsync(quiz, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenStaticQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new UpdateStaticQuizCommand(Guid.NewGuid(), "New Title", []);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.StaticQuizId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<StaticQuiz>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
