using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.Update;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class UpdateTopicHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly UpdateTopicHandler _handler;

        public UpdateTopicHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new UpdateTopicHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTopicFound_ShouldUpdateTopic()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new UpdateTopicCommand(topic.Id, "New Topic Name");
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            topic.Name.Should().Be(request.Name);

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
            var request = new UpdateTopicCommand(Guid.NewGuid(), "New Topic Name");
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
