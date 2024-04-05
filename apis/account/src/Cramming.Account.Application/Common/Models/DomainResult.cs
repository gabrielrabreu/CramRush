namespace Cramming.Account.Application.Common.Models
{
    public interface IDomainResult
    {
        bool Succeeded { get; set; }
        string[] Errors { get; set; }
    }

    public class DomainResult(bool succeeded, IEnumerable<string> errors) : IDomainResult
    {
        public bool Succeeded { get; set; } = succeeded;

        public string[] Errors { get; set; } = errors.ToArray();

        public static DomainResult Success()
        {
            return new DomainResult(true, []);
        }

        public static DomainResult Failure(IEnumerable<string> errors)
        {
            return new DomainResult(false, errors);
        }
    }
}
