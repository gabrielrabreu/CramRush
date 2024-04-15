using Cramming.Domain.StaticQuizAggregate;
using Cramming.UseCases.StaticQuizzes;
using Cramming.UseCases.StaticQuizzes.Get;

namespace Cramming.UnitTests.UseCases.StaticQuizzes
{
    public class GetStaticQuizHandlerTests
    {
        private readonly Mock<IStaticQuizReadRepository> _repositoryMock;
        private readonly GetStaticQuizHandler _handler;

        public GetStaticQuizHandlerTests()
        {
            _repositoryMock = new Mock<IStaticQuizReadRepository>();
            _handler = new GetStaticQuizHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenStaticQuizFound_ShouldReturnItem()
        {
            // Arrange
            var quiz = new StaticQuiz("Sample Title");
            var question = new StaticQuizQuestion("Sample Statement");
            var option = new StaticQuizQuestionOption("Sample Text", true);
            question.AddOption(option);
            quiz.AddQuestion(question);

            var request = new GetStaticQuizQuery(quiz.Id);
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
                    question.Statement.Should().Be(question.Statement);
                    question.Options.Should().HaveCount(1);
                    question.Options.Should().SatisfyRespectively(
                        option =>
                        {
                            option.Text.Should().Be(option.Text);
                            option.IsCorrect.Should().Be(option.IsCorrect);
                        });
                });

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.StaticQuizId, cancellationToken), Times.Once);
        }
    }
}
