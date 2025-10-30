namespace Common.Domain.Event;

/// <summary>
///     公共实体接口
/// </summary>
public interface IEvent
{
}

public interface IEvent<out TResponse>
{
}