namespace Common.Domain.State;

public interface IStateMachine
{
    ValueTask HandlerAsync(string json, CancellationToken token = default);
}