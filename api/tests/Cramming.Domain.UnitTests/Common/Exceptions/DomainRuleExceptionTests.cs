using Cramming.Domain.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;

namespace Cramming.Domain.UnitTests.Common.Exceptions
{
    public class DomainRuleExceptionTests
    {
        [Fact]
        public void Constructor_WithNoArguments_ShouldInitializeEmptyErrorsDictionary()
        {
            // Arrange & Act
            var exception = new DomainRuleException();

            // Assert
            exception.Errors.Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public void Constructor_WithValidationFailures_ShouldInitializeErrorsDictionaryCorrectly()
        {
            // Arrange
            var failures = new List<ValidationFailure>
            {
                new("PropertyName1", "Error message 1"),
                new("PropertyName2", "Error message 2"),
                new("PropertyName1", "Error message 3")
            };

            // Act
            var exception = new DomainRuleException(failures);

            // Assert
            exception.Errors.Should().NotBeNull()
                .And.HaveCount(2)
                .And.ContainKeys(["PropertyName1", "PropertyName2"]);
            
            exception.Errors["PropertyName1"].Should().NotBeNull()
                .And.HaveCount(2)
                .And.Contain("Error message 1")
                .And.Contain("Error message 3");
            
            exception.Errors["PropertyName2"].Should().NotBeNull()
                .And.HaveCount(1)
                .And.Contain("Error message 2");
        }

        [Fact]
        public void Constructor_WithPropertyNameAndErrorMessage_ShouldInitializeErrorsDictionaryCorrectly()
        {
            // Arrange
            var propertyName = "PropertyName";
            var errorMessage = "Error message";

            // Act
            var exception = new DomainRuleException(propertyName, errorMessage);

            // Assert
            exception.Errors.Should().NotBeNull()
                .And.HaveCount(1)
                .And.ContainKey(propertyName)
                .WhoseValue.Should().ContainSingle(errorMessage);
        }
    }
}
