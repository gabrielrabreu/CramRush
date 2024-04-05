using Cramming.Account.Domain.Common;
using Cramming.Account.Domain.Entities;

namespace Cramming.Account.Domain.Events
{
    public record SignedUpEvent(IApplicationUser User) : BaseEvent
    {
    }
}
