using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.UseCases.Topics.Create;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class CreateTopicHandlerTests
    {
        private readonly Mock<ITopicRepository> _repositoryMock;
        private readonly CreateTopicHandler _handler;

        public CreateTopicHandlerTests()
        {
            _repositoryMock = new Mock<ITopicRepository>();
            _handler = new CreateTopicHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewTopic()
        {
            // Arrange
            var topic = new Topic("Topic Name");

            var request = new CreateTopicCommand(topic.Name);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.AddAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value!.Id.Should().Be(topic.Id);
            result.Value.Name.Should().Be(topic.Name);

            _repositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Topic>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.AddAsync(It.Is<Topic>(topic => topic.Name == request.Name), cancellationToken), Times.Once);
        }
    }
}
