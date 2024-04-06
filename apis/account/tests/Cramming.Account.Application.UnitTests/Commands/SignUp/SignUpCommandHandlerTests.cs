using Cramming.Account.Application.Commands.SignUp;
using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.Common.Models;
using Cramming.Account.Domain.Entities;
using Cramming.Account.Domain.Events;
using FluentAssertions;
using MediatR;
using Moq;

namespace Cramming.Account.Application.UnitTests.Commands.SignUp
{
    public class SignUpCommandHandlerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<IPublisher> _publisher;
        private readonly SignUpCommandHandler _handler;

        public SignUpCommandHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _publisher = new Mock<IPublisher>();
            _handler = new SignUpCommandHandler(_identityService.Object, _publisher.Object);
        }

        [Fact]
        public async Task Handle_WhenCreateSucceed_ShouldPublishEventAndReturnResult()
        {
            // Arrange
            var command = new SignUpCommand() { UserName = "UserName", Email = "Email", Password = "Password" };
            var cancellationToken = new CancellationToken();

            var expectedResult = Mock.Of<IDomainResult>(e => e.Succeeded == true);
            var expectedUser = Mock.Of<IApplicationUser>();
            _identityService.Setup(e => e.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((expectedResult, expectedUser));

            // Act
            var actualResult = await _handler.Handle(command, cancellationToken);

            // Assert
            _identityService.Verify(e => e.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.CreateAsync(command.UserName!, command.Email!, command.Password!), Times.Once);

            _publisher.Verify(e => e.Publish(It.IsAny<SignedUpEvent>(), It.IsAny<CancellationToken>()), Times.Once);
            _publisher.Verify(e => e.Publish(It.Is<SignedUpEvent>(e => e.UserName == expectedUser.UserName), cancellationToken), Times.Once);

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public async Task Handle_WhenCreateFailed_ShouldJustReturnResult()
        {
            // Arrange
            var command = new SignUpCommand() { UserName = "UserName", Email = "Email", Password = "Password" };
            var cancellationToken = new CancellationToken();

            // Act
            var expectedResult = Mock.Of<IDomainResult>();
            var expectedUser = Mock.Of<IApplicationUser>();
            _identityService.Setup(e => e.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((expectedResult, expectedUser));

            var actualResult = await _handler.Handle(command, cancellationToken);

            // Assert
            _identityService.Verify(e => e.CreateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _identityService.Verify(e => e.CreateAsync(command.UserName!, command.Email!, command.Password!), Times.Once);

            _publisher.Verify(e => e.Publish(It.IsAny<SignedUpEvent>(), It.IsAny<CancellationToken>()), Times.Never);

            actualResult.Should().Be(expectedResult);
        }
    }
}
