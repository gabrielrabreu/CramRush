using Cramming.Domain.QuizAggregate;
using Cramming.UseCases.Quizzes;
using Cramming.UseCases.Quizzes.Get;

namespace Cramming.UnitTests.UseCases.Quizzes
{
    public class GetQuizHandlerTests
    {
        private readonly Mock<IQuizReadRepository> _repositoryMock;
        private readonly GetQuizHandler _handler;

        public GetQuizHandlerTests()
        {
            _repositoryMock = new Mock<IQuizReadRepository>();
            _handler = new GetQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenQuizFound_ShouldReturnItem()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");
            var quizQuestion = new QuizQuestion("Sample Statement");
            var quizQuestionOption = new QuizQuestionOption("Sample Text", true);
            quizQuestion.AddOption(quizQuestionOption);
            quiz.AddQuestion(quizQuestion);

            var request = new GetQuizQuery(quiz.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(quiz.Id);
            result.Value.Title.Should().Be(quiz.Title);
            result.Value.Questions.Should().HaveCount(1);
            result.Value.Questions.Should().SatisfyRespectively(
                question =>
                {
                    question.Id.Should().Be(quizQuestion.Id);
                    question.Statement.Should().Be(quizQuestion.Statement);
                    question.Options.Should().HaveCount(1);
                    question.Options.Should().SatisfyRespectively(
                        option =>
                        {
                            option.Id.Should().Be(quizQuestionOption.Id);
                            option.Text.Should().Be(quizQuestionOption.Text);
                            option.IsCorrect.Should().Be(quizQuestionOption.IsCorrect);
                        });
                });

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new GetQuizQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizId, cancellationToken), Times.Once);
        }
    }
}
