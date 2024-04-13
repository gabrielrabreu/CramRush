using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.UpdateTag;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class UpdateTagHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly UpdateTagHandler _handler;

        public UpdateTagHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new UpdateTagHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTopicAndTagFound_ShouldUpdateTag()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            var tag = topic.AddTag("Tag Name", "Tag Colour");

            var request = new UpdateTagCommand(topic.Id, tag.Id, "New Tag Name", "New Tag Colour");
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            tag.Name.Should().Be(request.Name);
            tag.Colour.Should().NotBeNull();
            tag.Colour!.Code.Should().Be(request.Colour);

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
            var request = new UpdateTagCommand(Guid.NewGuid(), Guid.NewGuid(), "New Tag Name", "New Tag Colour");
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

        [Fact]
        public async Task Handle_WhenTopicFoundButTagNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new UpdateTagCommand(topic.Id, Guid.NewGuid(), "New Tag Name", "New Tag Colour");
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

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
