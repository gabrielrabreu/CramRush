using Cramming.Account.Application.Common.Interfaces;
using Cramming.Account.Application.Queries.GetUsers;
using Cramming.Account.Domain.Entities;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Cramming.Account.Application.UnitTests.Queries.GetUsers
{
    public class GetUsersQueryHandlerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly GetUsersQueryHandler _handler;

        public GetUsersQueryHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _handler = new GetUsersQueryHandler(_identityService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedUsers()
        {
            // Arrange
            var query = new GetUsersQuery();
            var cancellationToken = new CancellationToken();

            var users = new List<IApplicationUser> { Mock.Of<IApplicationUser>() };
            _identityService.Setup(e => e.Users).Returns(users.BuildMock());

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            result.Items.Should().HaveCount(1);
        }
    }
}
