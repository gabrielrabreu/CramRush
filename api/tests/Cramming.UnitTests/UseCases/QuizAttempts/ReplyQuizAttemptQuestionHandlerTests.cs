using Cramming.Domain.QuizAttemptAggregate;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.ReplyQuestion;

namespace Cramming.UnitTests.UseCases.QuizAttempts
{
    public class ReplyQuizAttemptQuestionHandlerTests
    {
        private readonly Mock<IQuizAttemptRepository> _quizAttemptRepositoryMock;
        private readonly ReplyQuizAttemptQuestionHandler _handler;

        public ReplyQuizAttemptQuestionHandlerTests()
        {
            _quizAttemptRepositoryMock = new Mock<IQuizAttemptRepository>();
            _handler = new ReplyQuizAttemptQuestionHandler(_quizAttemptRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenQuizAttemptNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new ReplyQuizAttemptQuestionCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WhenQuizAttemptIsNotPending_ShouldReturnBadRequest()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);
            quizAttempt.MarkAnswer(quizAttemptQuestion, quizAttemptQuestionOption);

            var request = new ReplyQuizAttemptQuestionCommand(quizAttempt.Id, Guid.NewGuid(), Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            _quizAttemptRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.BadRequest);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WhenQuizAttemptQuestionNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);

            var request = new ReplyQuizAttemptQuestionCommand(quizAttempt.Id, Guid.NewGuid(), Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            _quizAttemptRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WhenQuizAttemptQuestionIsNotPending_ShouldReturnBadRequest()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion1 = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption1 = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion1.AddOption(quizAttemptQuestionOption1);
            quizAttempt.AddQuestion(quizAttemptQuestion1);
            quizAttempt.MarkAnswer(quizAttemptQuestion1, quizAttemptQuestionOption1);

            var quizAttemptQuestion2 = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption2 = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion2.AddOption(quizAttemptQuestionOption2);
            quizAttempt.AddQuestion(quizAttemptQuestion2);

            var request = new ReplyQuizAttemptQuestionCommand(quizAttempt.Id, quizAttemptQuestion1.Id, Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            _quizAttemptRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.BadRequest);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WhenQuizAttemptQuestionOptionNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);

            var request = new ReplyQuizAttemptQuestionCommand(quizAttempt.Id, quizAttemptQuestionOption.Id, Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            _quizAttemptRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WhenValid_ShouldReturnOk()
        {
            // Arrange
            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);

            var request = new ReplyQuizAttemptQuestionCommand(quizAttempt.Id, quizAttemptQuestionOption.Id, quizAttemptQuestionOption.Id);
            var cancellationToken = new CancellationToken();

            _quizAttemptRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quizAttempt);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizAttemptId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.UpdateAsync(
                quizAttempt,
                cancellationToken),
                Times.Once);
        }
    }
}
