using Cramming.Domain.StaticQuizAggregate;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Create;

namespace Cramming.UnitTests.UseCases.StaticQuizzes
{
    public class CreateStaticQuizHandlerTests
    {
        private readonly Mock<IStaticQuizRepository> _repositoryMock;
        private readonly CreateStaticQuizHandler _handler;

        public CreateStaticQuizHandlerTests()
        {
            _repositoryMock = new Mock<IStaticQuizRepository>();
            _handler = new CreateStaticQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewStaticQuiz()
        {
            // Arrange
            var quiz = new StaticQuiz("Sample Title");

            var request = new CreateStaticQuizCommand(
                quiz.Title,
                [
                    new CreateStaticQuizCommand.QuestionDto(
                        "Sample Statement",
                        [ new CreateStaticQuizCommand.QuestionOptionDto("Sample Text", true) ])
                ]);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.AddAsync(It.IsAny<StaticQuiz>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(quiz.Id);
            result.Value.Title.Should().Be(quiz.Title);

            _repositoryMock.Verify(mock => mock.AddAsync(It.IsAny<StaticQuiz>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.AddAsync(It.Is<StaticQuiz>(quiz => quiz.Title == request.Title), cancellationToken), Times.Once);
        }
    }
}
