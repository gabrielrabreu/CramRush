using Cramming.Account.Application.Commands.SignIn;
using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Domain.Entities;
using Cramming.Account.Domain.Events;
using FluentAssertions;
using MediatR;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cramming.Account.Application.UnitTests.Commands.SignIn
{
    public class SignInCommandHandlerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<IJwtService> _jwtService;
        private readonly Mock<IPublisher> _publisher;
        private readonly SignInCommandHandler _handler;

        public SignInCommandHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _jwtService = new Mock<IJwtService>();
            _publisher = new Mock<IPublisher>();
            _handler = new SignInCommandHandler(_identityService.Object, _jwtService.Object, _publisher.Object);
        }

        [Fact]
        public async Task Handle_WhenAuthenticateAsyncFailed_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var command = new SignInCommand() { UserName = "UserName", Password = "Password" };
            var cancellationToken = new CancellationToken();

            _identityService.Setup(e => e.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((false, null));

            // Act, Assert
            await FluentActions.Invoking(() => _handler.Handle(command, cancellationToken)).Should().ThrowAsync<UnauthorizedAccessException>();

            _identityService.Verify(e => e.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.AuthenticateAsync(command.UserName!, command.Password!), Times.Once);

            _publisher.Verify(e => e.Publish(It.IsAny<SignedInEvent>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenAuthenticateAsyncSucceed_ShouldReturnTokenResult()
        {
            // Arrange
            var command = new SignInCommand() { UserName = "UserName", Password = "Password" };
            var cancellationToken = new CancellationToken();

            var mockUser = Mock.Of<IApplicationUser>(b => b.Id == Guid.NewGuid());

            _identityService.Setup(e => e.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((true, mockUser));

            var claims = new List<Claim>();
            _identityService.Setup(e => e.ListClaimsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(claims);

            var accessToken = new JwtSecurityToken();
            _jwtService.Setup(e => e.CreateToken(It.IsAny<IList<Claim>>()))
                .Returns(accessToken);

            var refreshToken = "RefreshToken";
            _jwtService.Setup(e => e.GenerateRefreshToken())
                .Returns(refreshToken);

            var refreshTokenExpiryTime = DateTime.UtcNow;
            _jwtService.Setup(e => e.RefreshTokenExpiryTime())
                .Returns(refreshTokenExpiryTime);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            _identityService.Verify(e => e.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.AuthenticateAsync(command.UserName!, command.Password!), Times.Once);

            _identityService.Verify(e => e.ListClaimsAsync(It.IsAny<Guid>()), Times.Once);
            _identityService.Verify(e => e.ListClaimsAsync(mockUser.Id), Times.Once);

            _jwtService.Verify(e => e.CreateToken(It.IsAny<IList<Claim>>()), Times.Once);
            _jwtService.Verify(e => e.CreateToken(claims), Times.Once);

            _jwtService.Verify(e => e.GenerateRefreshToken(), Times.Once);

            _jwtService.Verify(e => e.RefreshTokenExpiryTime(), Times.Once);

            _identityService.Verify(e => e.UpdateRefreshTokenAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<DateTime?>()), Times.Once);
            _identityService.Verify(e => e.UpdateRefreshTokenAsync(mockUser.Id.ToString(), refreshToken, refreshTokenExpiryTime), Times.Once);

            _publisher.Verify(e => e.Publish(It.IsAny<SignedInEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            _publisher.Verify(e => e.Publish(It.Is<SignedInEvent>(e => e.UserName == command.UserName), cancellationToken), Times.Once);

            result.Should().NotBeNull();
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().Be(refreshToken);
            result.Expiration.Should().Be(accessToken.ValidTo);
        }
    }
}
