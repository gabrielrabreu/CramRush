using Cramming.Domain.Common;

namespace Cramming.Domain.ValueObjects
{
    public class Colour(string code) : ValueObject
    {
        public string Code { get; private set; } = string.IsNullOrWhiteSpace(code) ? "#000000" : code;

        public static Colour White => new("#FFFFFF");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
