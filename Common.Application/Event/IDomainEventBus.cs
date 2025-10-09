using Common.Domain.Event;

namespace Common.Application.Event;

public interface IDomainEventBus : IEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IEventDomain;
}