using Cramming.SharedKernel;

namespace Cramming.UnitTests.SharedKernel
{
    public class LoggingBehaviourTests
    {
        private readonly Mock<ILogger<Mediator>> _loggerMock;
        private readonly Mock<RequestHandlerDelegate<MyResponse>> _nextMock;
        private readonly LoggingBehaviour<MyRequest, MyResponse> _behaviour;

        public LoggingBehaviourTests()
        {
            _loggerMock = new Mock<ILogger<Mediator>>();
            _nextMock = new Mock<RequestHandlerDelegate<MyResponse>>();
            _behaviour = new LoggingBehaviour<MyRequest, MyResponse>(_loggerMock.Object);
        }

        [Fact]
        public async Task Process_ShouldLogRequestAndResponse()
        {
            // Arrange
            var request = new MyRequest();
            var response = new MyResponse();
            var cancellationToken = new CancellationToken();

            _nextMock.Setup(handler => handler()).ReturnsAsync(response);

            // Act
            var result = await _behaviour.Handle(request, _nextMock.Object, cancellationToken);

            // Assert
            result.Should().Be(response);

            _loggerMock.VerifyLog(
                LogLevel.Information,
                "Handling {RequestName}",
                new KeyValuePair<string, object>("RequestName", typeof(MyRequest).Name));

            _nextMock.Verify(handler => handler(), Times.Once);

            _loggerMock.VerifyLog(
                LogLevel.Information,
                "Handled {RequestName} with {Response} in {ms} ms",
                new KeyValuePair<string, object>("RequestName", typeof(MyRequest).Name),
                new KeyValuePair<string, object>("Response", response));
        }

        public record MyRequest : IRequest<MyResponse> { }

        public record MyResponse { }
    }
}
