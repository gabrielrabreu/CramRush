using Cramming.SharedKernel;

namespace Cramming.UnitTests.SharedKernel
{
    public class ValueObjectTests
    {
        public class SampleValueObject(int value) : ValueObject
        {
            public int Value { get; } = value;

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Value;
            }
        }

        [Fact]
        public void Equals_ReturnsTrue_WhenObjectsAreEqual()
        {
            // Arrange
            var obj1 = new SampleValueObject(10);
            var obj2 = new SampleValueObject(10);

            // Act & Assert
            obj1.Equals(obj2).Should().BeTrue();
            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenObjectsAreNotEqual()
        {
            // Arrange
            var obj1 = new SampleValueObject(10);
            var obj2 = new SampleValueObject(20);

            // Act & Assert
            obj1.Equals(obj2).Should().BeFalse();
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparingWithDifferentType()
        {
            // Arrange
            var obj = new SampleValueObject(10);
            var differentTypeObj = new object();

            // Act & Assert
            obj.Equals(differentTypeObj).Should().BeFalse();
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparingWithNull()
        {
            // Arrange
            var obj = new SampleValueObject(10);

            // Act & Assert
            obj.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_ReturnsSameValue_ForEqualObjects()
        {
            // Arrange
            var obj1 = new SampleValueObject(10);
            var obj2 = new SampleValueObject(10);

            // Act & Assert
            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentValue_ForDifferentObjects()
        {
            // Arrange
            var obj1 = new SampleValueObject(10);
            var obj2 = new SampleValueObject(20);

            // Act & Assert
            obj1.GetHashCode().Should().NotBe(obj2.GetHashCode());
        }
    }
}
