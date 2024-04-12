using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.Delete;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class DeleteTopicHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly DeleteTopicHandler _handler;

        public DeleteTopicHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new DeleteTopicHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFoundTopic_ShouldDeleteTopic()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new DeleteTopicCommand(topic.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.DeleteAsync(topic, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new DeleteTopicCommand(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
