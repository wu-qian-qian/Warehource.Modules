namespace Common.Domain.Event;

public abstract class IMassTransitDomainEvent
{
    protected IMassTransitDomainEvent(DateTime occurredOnUtc)
    {
        OccurredOnUtc = occurredOnUtc;
    }

    public DateTime OccurredOnUtc { get; }

    public Guid? EventId { get; set; }
}