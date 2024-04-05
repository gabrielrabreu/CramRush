using Cramming.Account.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cramming.Account.Application.Events
{
    public class SignedUpEventHandler(ILogger<SignedUpEventHandler> logger) : INotificationHandler<SignedUpEvent>
    {
        public Task Handle(SignedUpEvent @event, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event: {DomainEvent}", @event.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
