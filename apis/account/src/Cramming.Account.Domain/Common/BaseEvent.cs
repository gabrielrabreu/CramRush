using MediatR;

namespace Cramming.Account.Domain.Common
{
    public abstract record BaseEvent : INotification
    {
    }
}
