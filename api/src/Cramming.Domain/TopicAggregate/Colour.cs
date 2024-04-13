using Cramming.SharedKernel;

namespace Cramming.Domain.TopicAggregate
{
    public class Colour(string? code) : ValueObject
    {
        public string Code { get; private set; } = string.IsNullOrWhiteSpace(code) ? "#FFFFFF" : code;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
