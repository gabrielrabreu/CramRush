using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.Get;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class GetTopicHandlerTests
    {
        private readonly Mock<ITopicReadRepository> _repositoryMock;
        private readonly GetTopicHandler _handler;

        public GetTopicHandlerTests()
        {
            _repositoryMock = new Mock<ITopicReadRepository>();
            _handler = new GetTopicHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTopicFound_ShouldReturnItem()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new GetTopicQuery(topic.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(topic.Id);
            result.Value.Name.Should().Be(topic.Name);
            result.Value.Tags.Should().BeEmpty();
            result.Value.Questions.Should().BeEmpty();

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicFoundWithTags_ShouldReturnItem()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            var tag = topic.AddTag("Tag Name", "Tag Colour");

            var request = new GetTopicQuery(topic.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(topic.Id);
            result.Value.Name.Should().Be(topic.Name);
            result.Value.Tags.Should().HaveCount(1);
            result.Value.Tags.Should().SatisfyRespectively(first =>
            {
                first.Id.Should().Be(tag.Id);
                first.TopicId.Should().Be(tag.TopicId);
                first.Name.Should().Be(tag.Name);
                first.Colour.Should().Be(tag.Colour.Code);
            });
            result.Value.Questions.Should().BeEmpty();

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicFoundWithOpenEndedQuestion_ShouldReturnItem()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            var question = topic.AddOpenEndedQuestion("Question Statement", "Question Answer");

            var request = new GetTopicQuery(topic.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(topic.Id);
            result.Value.Name.Should().Be(topic.Name);
            result.Value.Tags.Should().BeEmpty();
            result.Value.Questions.Should().HaveCount(1);
            result.Value.Questions.Should().SatisfyRespectively(first =>
            {
                first.Id.Should().Be(question.Id);
                first.TopicId.Should().Be(question.TopicId);
                first.Type.Should().Be(QuestionType.OpenEnded);
                first.Statement.Should().Be(question.Statement);
                first.Answer.Should().Be(question.Answer);
                first.Options.Should().BeEmpty();
            });

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);
        }


        [Fact]
        public async Task Handle_WhenTopicFoundWithMultipleChoiceQuestion_ShouldReturnItem()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            var question = topic.AddMultipleChoiceQuestion("Question Statement");
            var option = question.AddOption("Option Statement", true);

            var request = new GetTopicQuery(topic.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(topic.Id);
            result.Value.Name.Should().Be(topic.Name);
            result.Value.Tags.Should().BeEmpty();
            result.Value.Questions.Should().HaveCount(1);
            result.Value.Questions.Should().SatisfyRespectively(first =>
            {
                first.Id.Should().Be(question.Id);
                first.TopicId.Should().Be(question.TopicId);
                first.Type.Should().Be(QuestionType.MultipleChoice);
                first.Statement.Should().Be(question.Statement);
                first.Answer.Should().BeNull();
                first.Options.Should().HaveCount(1);
                first.Options.Should().SatisfyRespectively(firstOption =>
                {
                    firstOption.Id.Should().Be(option.Id);
                    firstOption.QuestionId.Should().Be(option.QuestionId);
                    firstOption.Statement.Should().Be(option.Statement);
                    firstOption.IsAnswer.Should().Be(option.IsAnswer);
                });
            });

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new GetTopicQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);
        }
    }
}
