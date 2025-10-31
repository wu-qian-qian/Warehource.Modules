using Common.Domain.Event;

namespace Common.Application.Event;

/// <summary>
///     事件总线接口
/// </summary>
public interface IEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IEvent;

    [Obsolete("内部使用dynamic不推荐使用")]
    Task<TResponse> PublishAsync2<TResponse>(IEvent<TResponse> integrationEvent,
        CancellationToken cancellationToken = default);

    Task<TResponse> PublishAsync<TResponse>(
        IEvent<TResponse> @event,
        CancellationToken ct = default);
}