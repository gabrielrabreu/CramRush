using Cramming.Domain.QuizAggregate;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.Create;

namespace Cramming.UnitTests.UseCases.Quizzes
{
    public class CreateQuizHandlerTests
    {
        private readonly Mock<IQuizRepository> _repositoryMock;
        private readonly CreateQuizHandler _handler;

        public CreateQuizHandlerTests()
        {
            _repositoryMock = new Mock<IQuizRepository>();
            _handler = new CreateQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewQuiz()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");

            var request = new CreateQuizCommand(
                quiz.Title,
                [
                    new CreateQuizCommand.QuestionDto(
                        "Sample Statement",
                        [ new CreateQuizCommand.QuestionOptionDto("Sample Text", true) ])
                ]);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Quiz>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(quiz.Id);
            result.Value.Title.Should().Be(quiz.Title);

            _repositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Quiz>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.AddAsync(It.Is<Quiz>(quiz => quiz.Title == request.Title), cancellationToken), Times.Once);
        }
    }
}
