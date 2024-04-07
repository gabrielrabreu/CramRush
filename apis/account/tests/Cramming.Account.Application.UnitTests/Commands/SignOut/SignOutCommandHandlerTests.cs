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
        public async Task Handle_ShouldCallRevokeRefreshTokens()
        {
            // Arrange
            var command = new SignOutCommand("UserId");
            var cancellationToken = new CancellationToken();

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _identityService.Verify(e => e.RevokeRefreshToken(It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.RevokeRefreshToken(command.UserId), Times.Once);
        }
    }
}
