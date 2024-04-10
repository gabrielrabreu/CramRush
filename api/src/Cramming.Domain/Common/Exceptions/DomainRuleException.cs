using FluentValidation.Results;

namespace Cramming.Domain.Common.Exceptions
{
    public class DomainRuleException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public DomainRuleException()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public DomainRuleException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(k => k.Key, k => k.ToArray());
        }

        public DomainRuleException(string propertyName, string errorMessage) : this()
        {
            Errors = new Dictionary<string, string[]>
            {
                { propertyName, new[] { errorMessage } }
            };
        }
    }
}
