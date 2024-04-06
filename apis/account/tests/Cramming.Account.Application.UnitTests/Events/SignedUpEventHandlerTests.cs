using Cramming.Account.Application.Events;
using Cramming.Account.Application.UnitTests.Support;
using Cramming.Account.Domain.Entities;
using Cramming.Account.Domain.Events;
using Microsoft.Extensions.Logging;
using Moq;

namespace Cramming.Account.Application.UnitTests.Events
{
    public class SignedUpEventHandlerTests
    {
        private readonly Mock<ILogger<SignedUpEventHandler>> _logger;
        private readonly SignedUpEventHandler _handler;

        public SignedUpEventHandlerTests()
        {
            _logger = new Mock<ILogger<SignedUpEventHandler>>();
            _handler = new SignedUpEventHandler(_logger.Object);
        }

        [Fact]
        public async Task Handle_ShouldLogInformation()
        {
            // Arrange
            var @event = new SignedUpEvent(string.Empty);
            var cancellationToken = new CancellationToken();

            // Act
            await _handler.Handle(@event, cancellationToken);

            // Assert
            var expectedLog = $"Domain Event: {@event.GetType().Name}";
            _logger.VerifyLogInformation(expectedLog);
        }
    }
}
