using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.ReplaceQuestions;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class ReplaceQuestionsHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly ReplaceQuestionsHandler _handler;

        public ReplaceQuestionsHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new ReplaceQuestionsHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTopicFound_ShouldReplaceQuestions()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            topic.AddOpenEndedQuestion("Question Statement", "Question Answer");

            var request = new ReplaceQuestionsCommand(topic.Id, []);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            topic.Questions.Should().BeEmpty();

            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.UpdateAsync(topic, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicFoundWithOpenEndedQuestion_ShouldReplaceQuestions()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new ReplaceQuestionsCommand(topic.Id, [
                new ReplaceQuestionsCommand.CreateQuestion(QuestionType.OpenEnded, "Question Statement", "Question Answer", [])
            ]);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            topic.Questions.Should().HaveCount(1);

            var question = topic.Questions.Single();
            question.Should().BeOfType<OpenEndedQuestion>();

            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.UpdateAsync(topic, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicFoundWithMultipleChoiceQuestion_ShouldReplaceQuestions()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new ReplaceQuestionsCommand(topic.Id, [
                new ReplaceQuestionsCommand.CreateQuestion(QuestionType.MultipleChoice, "Question Statement", "Question Answer", [
                    new ReplaceQuestionsCommand.CreateMultipleChoiceQuestionOption("Option Statement 1", true),
                    new ReplaceQuestionsCommand.CreateMultipleChoiceQuestionOption("Option Statement 2", true)])
            ]);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            topic.Questions.Should().HaveCount(1);

            var question = topic.Questions.Single();
            question.Should().BeOfType<MultipleChoiceQuestion>().Which.Options.Should().HaveCount(2);

            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.UpdateAsync(topic, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new ReplaceQuestionsCommand(Guid.NewGuid(), []);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
