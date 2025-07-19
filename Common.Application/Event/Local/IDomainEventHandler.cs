using Common.Domain.Event;

namespace Common.Application.Event.Local;

public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
    where TDomainEvent : IEventDomain
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

public interface IDomainEventHandler
{
    Task Handle(IEventDomain domainEvent, CancellationToken cancellationToken = default);
}