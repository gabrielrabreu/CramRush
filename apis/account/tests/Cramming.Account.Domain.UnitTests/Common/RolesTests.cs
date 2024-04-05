using Cramming.Account.Domain.Common;
using FluentAssertions;

namespace Cramming.Account.Domain.UnitTests.Common
{
    public class RolesTests
    {
        [Fact]
        public void GetAlLRoles_ShouldReturnAlLExpectedRoles()
        {
            // Act
            var allRoles = Roles.GetAllRoles();

            // Assert
            allRoles.Should().HaveCount(2);
            allRoles.Should().Contain(nameof(Roles.Administrator));
            allRoles.Should().Contain(nameof(Roles.User));
        }
    }
}
