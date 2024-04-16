using Cramming.Domain.QuizAggregate;
using Cramming.Domain.QuizAttemptAggregate;
using Cramming.UseCases.QuizAttempts;
using Cramming.UseCases.QuizAttempts.Create;
using Cramming.UseCases.Quizzes;

namespace Cramming.UnitTests.UseCases.QuizAttempts
{
    public class CreateQuizAttemptHandlerTests
    {
        private readonly Mock<IQuizReadRepository> _quizRepositoryMock;
        private readonly Mock<IQuizAttemptRepository> _quizAttemptRepositoryMock;
        private readonly CreateQuizAttemptHandler _handler;

        public CreateQuizAttemptHandlerTests()
        {
            _quizRepositoryMock = new Mock<IQuizReadRepository>();
            _quizAttemptRepositoryMock = new Mock<IQuizAttemptRepository>();
            _handler = new CreateQuizAttemptHandler(_quizRepositoryMock.Object, _quizAttemptRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewQuizAttempt()
        {
            // Arrange
            var quiz = new Quiz("Sample Title");
            var quizQuestion = new QuizQuestion("Sample Statement");
            var quizQuestionOption = new QuizQuestionOption("Sample Text", true);
            quizQuestion.AddOption(quizQuestionOption);
            quiz.AddQuestion(quizQuestion);

            var quizAttempt = new QuizAttempt("Sample Title");
            var quizAttemptQuestion = new QuizAttemptQuestion("Sample Statement");
            var quizAttemptQuestionOption = new QuizAttemptQuestionOption("Sample Text", true);
            quizAttemptQuestion.AddOption(quizAttemptQuestionOption);
            quizAttempt.AddQuestion(quizAttemptQuestion);

            var request = new CreateQuizAttemptCommand(quiz.Id);
            var cancellationToken = new CancellationToken();

            _quizRepositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(quiz);

            _quizAttemptRepositoryMock.Setup(mock => mock.AddAsync(It.IsAny<QuizAttempt>(), It.IsAny<CancellationToken>()))
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

            _quizRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()), 
                Times.Once);
            
            _quizRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizId, 
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.AddAsync(
                It.IsAny<QuizAttempt>(), 
                It.IsAny<CancellationToken>()), 
                Times.Once);
            
            _quizAttemptRepositoryMock.Verify(mock => mock.AddAsync(
                It.Is<QuizAttempt>(quizAttempt => quizAttempt.QuizTitle == quiz.Title), 
                cancellationToken), 
                Times.Once);
        }

        [Fact]
        public async Task Handle_WhenQuizNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new CreateQuizAttemptCommand(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _quizRepositoryMock.Verify(mock => mock.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
                Times.Once);

            _quizRepositoryMock.Verify(mock => mock.GetByIdAsync(
                request.QuizId,
                cancellationToken),
                Times.Once);

            _quizAttemptRepositoryMock.Verify(mock => mock.AddAsync(
                It.IsAny<QuizAttempt>(),
                It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
