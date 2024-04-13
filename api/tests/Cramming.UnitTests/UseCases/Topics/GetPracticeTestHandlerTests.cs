using Cramming.Domain.TopicAggregate;
using Cramming.Domain.TopicAggregate.Repositories;
using Cramming.SharedKernel;
using Cramming.UseCases.Topics.GetPracticeTest;

namespace Cramming.UnitTests.UseCases.Topics
{
    public class GetPracticeTestHandlerTests
    {
        private readonly Mock<ITopicReadRepository> _repositoryMock;
        private readonly Mock<IPracticeTestPdfService> _serviceMock;
        private readonly GetPracticeTestHandler _handler;

        public GetPracticeTestHandlerTests()
        {
            _repositoryMock = new Mock<ITopicReadRepository>();
            _serviceMock = new Mock<IPracticeTestPdfService>();
            _handler = new GetPracticeTestHandler(_repositoryMock.Object, _serviceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTopicFound_ShouldReturnDocument()
        {
            // Arrange
            var topic = new Topic("Topic Name");
            var document = new BinaryContent([], "Content Type", "Name");

            var request = new GetPracticeTestQuery(topic.Id);
            var cancellationToken = new CancellationToken();

            _repositoryMock.Setup(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(topic);

            _serviceMock.Setup(mock => mock.Create(It.IsAny<Topic>()))
                .Returns(document);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.OK);
            result.Value.Should().NotBeNull();
            result.Value.Should().Be(document);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _serviceMock.Verify(mock => mock.Create(It.IsAny<Topic>()), Times.Once);
            _serviceMock.Verify(mock => mock.Create(topic), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTopicNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = new GetPracticeTestQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(HttpStatusCode.NotFound);

            _repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(mock => mock.GetByIdAsync(request.TopicId, cancellationToken), Times.Once);

            _serviceMock.Verify(mock => mock.Create(It.IsAny<Topic>()), Times.Never);
        }
    }
}
