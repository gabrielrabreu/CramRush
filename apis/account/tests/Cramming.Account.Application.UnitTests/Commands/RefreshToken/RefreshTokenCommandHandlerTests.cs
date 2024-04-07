using Cramming.Account.Application.Commands.RefreshToken;
using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Domain.Entities;
using FluentAssertions;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cramming.Account.Application.UnitTests.Commands.RefreshToken
{
    public class RefreshTokenCommandHandlerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<IJwtService> _jwtService;
        private readonly RefreshTokenCommandHandler _handler;

        public RefreshTokenCommandHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _jwtService = new Mock<IJwtService>();
            _handler = new RefreshTokenCommandHandler(_identityService.Object, _jwtService.Object);
        }

        [Fact]
        public async Task Handle_WhenUserNotFound_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var command = new RefreshTokenCommand() { AccessToken = "AccessToken", RefreshToken = "RefreshToken" };
            var cancellationToken = new CancellationToken();

            var claims = new ClaimsPrincipal();
            var identifier = "UserId";

            _jwtService.Setup(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()))
                .Returns((claims, identifier));

            // Act, Assert
            await FluentActions.Invoking(() => _handler.Handle(command, cancellationToken)).Should().ThrowAsync<UnauthorizedAccessException>();

            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()), Times.Once);
            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(command.AccessToken!), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenDifferentRefreshToken_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var command = new RefreshTokenCommand() { AccessToken = "AccessToken", RefreshToken = "RefreshToken" };
            var cancellationToken = new CancellationToken();

            var claims = new ClaimsPrincipal();
            var identifier = "UserId";
            var user = Mock.Of<IApplicationUser>(b => b.RefreshToken == "OtherRefreshToken");

            _jwtService.Setup(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()))
                .Returns((claims, identifier));

            _identityService.Setup(e => e.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act, Assert
            await FluentActions.Invoking(() => _handler.Handle(command, cancellationToken)).Should().ThrowAsync<UnauthorizedAccessException>();

            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()), Times.Once);
            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(command.AccessToken!), Times.Once);

            _identityService.Verify(e => e.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.FindByIdAsync(identifier), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenInvalidRefreshTokenExpiryTime_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var command = new RefreshTokenCommand() { AccessToken = "AccessToken", RefreshToken = "RefreshToken" };
            var cancellationToken = new CancellationToken();

            var claims = new ClaimsPrincipal();
            var identifier = "UserId";
            var user = Mock.Of<IApplicationUser>(b =>
                b.RefreshToken == command.RefreshToken 
                && b.RefreshTokenExpiryTime == DateTime.UtcNow.AddMinutes(-5));

            _jwtService.Setup(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()))
                .Returns((claims, identifier));

            _identityService.Setup(e => e.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act, Assert
            await FluentActions.Invoking(() => _handler.Handle(command, cancellationToken)).Should().ThrowAsync<UnauthorizedAccessException>();

            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()), Times.Once);
            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(command.AccessToken!), Times.Once);

            _identityService.Verify(e => e.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.FindByIdAsync(identifier), Times.Once);
        }


        [Fact]
        public async Task Handle_WhenValidUserAndRefreshData_ShouldReturnTokenResult()
        {
            // Arrange
            var command = new RefreshTokenCommand() { AccessToken = "AccessToken", RefreshToken = "RefreshToken" };
            var cancellationToken = new CancellationToken();

            var principal = new ClaimsPrincipal();
            var identifier = "UserId";
            var mockUser = Mock.Of<IApplicationUser>(b => 
                b.Id == Guid.NewGuid()
                && b.RefreshToken == command.RefreshToken 
                && b.RefreshTokenExpiryTime == DateTime.UtcNow.AddMinutes(5));

            _jwtService.Setup(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()))
                .Returns((principal, identifier));

            _identityService.Setup(e => e.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(mockUser);

            var accessToken = new JwtSecurityToken();
            _jwtService.Setup(e => e.CreateToken(It.IsAny<IList<Claim>>()))
                .Returns(accessToken);

            var newRefreshToken = "NewRefreshToken";
            _jwtService.Setup(e => e.GenerateRefreshToken())
                .Returns(newRefreshToken);

            var newRefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(15);
            _jwtService.Setup(e => e.RefreshTokenExpiryTime())
                .Returns(newRefreshTokenExpiryTime);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(It.IsAny<string>()), Times.Once);
            _jwtService.Verify(e => e.GetPrincipalFromExpiredToken(command.AccessToken!), Times.Once);

            _identityService.Verify(e => e.FindByIdAsync(It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.FindByIdAsync(identifier), Times.Once);

            _jwtService.Verify(e => e.CreateToken(It.IsAny<IList<Claim>>()), Times.Once);
            _jwtService.Verify(e => e.CreateToken(principal.Claims.ToList()), Times.Once);

            _jwtService.Verify(e => e.GenerateRefreshToken(), Times.Once);

            _jwtService.Verify(e => e.RefreshTokenExpiryTime(), Times.Once);

            _identityService.Verify(e => e.UpdateRefreshTokenAsync(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<DateTime?>()), Times.Once);
            _identityService.Verify(e => e.UpdateRefreshTokenAsync(mockUser.Id.ToString(), newRefreshToken, newRefreshTokenExpiryTime), Times.Once);

            result.Should().NotBeNull();
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().Be(newRefreshToken);
            result.Expiration.Should().Be(accessToken.ValidTo);
        }
    }
}
