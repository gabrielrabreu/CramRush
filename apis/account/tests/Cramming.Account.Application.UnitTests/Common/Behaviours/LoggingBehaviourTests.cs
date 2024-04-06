using Cramming.Account.Application.Common.Behaviours;
using Cramming.Account.Application.UnitTests.Support;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cramming.Account.Application.UnitTests.Common.Behaviours
{
    public class TestCommand : IRequest { }

    public class LoggingBehaviourTests
    {
        private readonly Mock<ILogger<TestCommand>> _logger;
        private readonly LoggingBehaviour<TestCommand> _behaviour;

        public LoggingBehaviourTests()
        {
            _logger = new Mock<ILogger<TestCommand>>();
            _behaviour = new LoggingBehaviour<TestCommand>(_logger.Object);
        }

        [Fact]
        public async Task Process_ShouldLogInformation()
        {
            // Arrange
            var request = new TestCommand();
            var cancellationToken = new CancellationToken();

            var requestName = typeof(TestCommand).Name;
            var userId = string.Empty;
            var userName = string.Empty;

            // Act
            await _behaviour.Process(request, cancellationToken);

            // Assert
            var expectedLog = $"Notify Request: {requestName} {userId} {userName} {request}";
            _logger.VerifyLogInformation(expectedLog);
        }
    }
}
