using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.CreateTag;
using FluentAssertions;
using Moq;
using System.Net;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class CreateTagHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly CreateTagHandler _handler;

        public CreateTagHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new CreateTagHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFoundTopic_ShouldCreateNewTag()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new CreateTagCommand(topic.Id, "Tag Name", "Tag Colour");
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            topic.Tags.Should().HaveCount(1);
            var tag = topic.Tags.Single();

            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(tag.Id);
            result.Value.TopicId.Should().Be(tag.TopicId);
            result.Value.Name.Should().Be(tag.Name);
            result.Value.Colour.Should().Be(tag.Colour.Code);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.UpdateAsync(topic, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new CreateTagCommand(Guid.NewGuid(), "Tag Name", "Tag Colour");
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
