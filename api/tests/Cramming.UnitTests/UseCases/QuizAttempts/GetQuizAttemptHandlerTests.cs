using Cramming.Domain.QuizAttemptAggregate;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.Get;

namespace Cramming.UnitTests.UseCases.QuizAttempts
{
    public class GetQuizAttemptHandlerTests
    {
        private readonly Mock<IQuizAttemptReadRepository> _repositoryMock;
        private readonly GetQuizAttemptHandler _handler;

        public GetQuizAttemptHandlerTests()
        {
            _repositoryMock = new Mock<IQuizAttemptReadRepository>();
            _handler = new GetQuizAttemptHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenQuizFound_ShouldReturnItem()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);

            var request = new GetQuizAttemptQuery(quizAttempt.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(quizAttempt.Id);
            result.Value.QuizTitle.Should().Be(quizAttempt.QuizTitle);
            result.Value.IsPending.Should().Be(quizAttempt.IsPending);
            result.Value.Questions.Should().HaveCount(1);
            result.Value.Questions.Should().SatisfyRespectively(
                question =>
                {
                    question.Id.Should().Be(quizAttemptQuestion.Id);
                    question.Statement.Should().Be(quizAttemptQuestion.Statement);
                    question.IsPending.Should().Be(quizAttemptQuestion.IsPending);
                    question.Options.Should().HaveCount(1);
                    question.Options.Should().SatisfyRespectively(
                        option =>
                        {
                            option.Id.Should().Be(quizAttemptQuestionOption.Id);
                            option.Text.Should().Be(quizAttemptQuestionOption.Text);
                            option.IsSelected.Should().Be(quizAttemptQuestionOption.IsSelected);
                        });
                });

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizAttemptId, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new GetQuizAttemptQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.QuizAttemptId, cancellationToken), Times.Once);
        }
    }
}
