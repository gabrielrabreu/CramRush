using MediatR;

namespace Cramming.Domain.Common
{
    public abstract record BaseEvent : INotification
    {
    }
}
