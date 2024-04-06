using Cramming.Account.Domain.Common;

namespace Cramming.Account.Domain.Events
{
    public record SignedInEvent(string UserName) : BaseEvent
    {
    }
}
