namespace Common.Domain.State;

public interface IStateMachine
{
    ValueTask HandlerAsync(string deviceName, CancellationToken token = default);
}