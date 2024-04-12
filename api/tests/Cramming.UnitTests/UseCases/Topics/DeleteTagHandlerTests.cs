using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.DeleteTag;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class DeleteTagHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly DeleteTagHandler _handler;

        public DeleteTagHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new DeleteTagHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTopicAndTagFound_ShouldDeleteTag()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            var tag = topic.AddTag("Tag Name", "Tag Colour");

            var request = new DeleteTagCommand(topic.Id, tag.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            topic.Tags.Should().BeEmpty();

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
            var request = new DeleteTagCommand(Guid.NewGuid(), Guid.NewGuid());
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

            var request = new DeleteTagCommand(topic.Id, Guid.NewGuid());
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
