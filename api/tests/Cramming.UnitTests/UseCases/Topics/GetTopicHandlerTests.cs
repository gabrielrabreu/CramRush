using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.Get;
using FluentAssertions;
using Moq;
using System.Net;

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
