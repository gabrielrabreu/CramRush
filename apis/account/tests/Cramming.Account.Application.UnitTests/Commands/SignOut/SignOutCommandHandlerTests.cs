using Cramming.Account.Application.Commands.SignOut;
using Cramming.Account.Application.Common.Interfaces;
using Moq;

namespace Cramming.Account.Application.UnitTests.Commands.SignOut
{
    public class SignOutCommandHandlerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly SignOutCommandHandler _handler;

        public SignOutCommandHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _handler = new SignOutCommandHandler(_identityService.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateRefreshToken()
        {
            // Arrange
            var command = new SignOutCommand("UserId");
            var cancellationToken = new CancellationToken();

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _identityService.Verify(e => e.UpdateRefreshTokenAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<DateTime?>()), Times.Once);
            _identityService.Verify(e => e.UpdateRefreshTokenAsync(command.UserId, null, null), Times.Once);
        }
    }
}
